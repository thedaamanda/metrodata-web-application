using WebApp.Context;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories.Data;

public class AccountRepository : GeneralRepository<string, Account, MyContext>, IAccountRepository
{
    public AccountRepository(MyContext context) : base(context)
    {
    }

    public int Register(RegisterVM registerVM)
    {
        int result = 0;

        var transaction = _context.Database.BeginTransaction();

        try {
            // Make Generate NIK
            Random random = new Random();

            do {
                registerVM.NIK = Convert.ToString(random.Next(10000, 99999));
            } while (_context.Employees.Any(e => e.NIK == registerVM.NIK));

            // Check if email or phone number already exist
            if (CheckEmailPhone(registerVM.Email, registerVM.PhoneNumber))
            {
                var university = new University {
                    Name = registerVM.UniversityName,
                };

                // Validate if university already exist
                if (_context.Universities.Any(u => u.Name == university.Name)) {
                    university.Id = _context.Universities.FirstOrDefault(u => u.Name == university.Name).Id;
                } else {
                    _context.Universities.Add(university);
                    _context.SaveChanges();
                }

                var education = new Education {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityId = university.Id,
                };
                _context.Educations.Add(education);
                _context.SaveChanges();

                var employee = new Employee {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    PhoneNumber = registerVM.PhoneNumber,
                    Email = registerVM.Email,
                    HiringDate = DateTime.Now
                };
                _context.Employees.Add(employee);
                _context.SaveChanges();

                var account = new Account {
                    EmployeeNIK = registerVM.NIK,
                    Password = registerVM.Password,
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();

                var accountRole = new AccountRole {
                    AccountNIK = registerVM.NIK,
                    RoleId = 2
                };
                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();

                var profiling = new Profiling {
                    EmployeeNIK = registerVM.NIK,
                    EducationId = education.Id,
                };
                _context.Profilings.Add(profiling);
                result = _context.SaveChanges();

                transaction.Commit();
            }
        } catch (Exception e) {
            transaction.Rollback();
            throw new Exception(e.Message);
        }

        return result;
    }

    public bool Login(LoginVM loginVM)
    {
        var getAccount = _context.Accounts.Join(
            _context.Employees,
            a => a.EmployeeNIK,
            e => e.NIK,
            (a, e) => new LoginVM {
                Email = e.Email,
                Password = a.Password,
            }).FirstOrDefault(a => a.Email == loginVM.Email && a.Password == loginVM.Password);

        if (getAccount is null) {
            return false;
        }

        return true;
    }

    public bool CheckEmailPhone(string email, string phone)
    {
        return _context.Employees.Where(e => e.Email == email || e.PhoneNumber == phone).SingleOrDefault() == null;
    }

    public UserdataVM  GetUserData(string email)
    {
        var userdata = (from e in _context.Employees
                                   join a in _context.Accounts
                                   on e.NIK equals a.EmployeeNIK
                                   join ar in _context.AccountRoles
                                   on a.EmployeeNIK equals ar.AccountNIK
                                   join r in _context.Roles
                                   on ar.RoleId equals r.Id
                                   where e.Email == email
                                   select new UserdataVM
                                   {
                                       Email = e.Email,
                                       FullName = String.Concat(e.FirstName, " ", e.LastName),
                                       Role = r.Name
                                   }).FirstOrDefault();

        return userdata;
    }
}
