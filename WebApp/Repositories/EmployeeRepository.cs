using WebApp.Models;
using WebApp.Context;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories;

public class EmployeeRepository : IRepository<string, Employee>
{
    private readonly MyContext _context;

    public EmployeeRepository(MyContext context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _context.Set<Employee>().ToList();
    }

    public Employee? GetById(string nik)
    {
        return _context.Set<Employee>().Find(nik);
    }

    public int Insert(Employee employee)
    {
        _context.Set<Employee>().Add(employee);
        return _context.SaveChanges();
    }

    public IEnumerable<Employee> Search(string name)
    {
        return GetAll().Where(
            x => x.FirstName.Contains(name) || x.LastName.Contains(name)
        );
    }

    public int Update(Employee employee)
    {
        _context.Set<Employee>().Update(employee);
        return _context.SaveChanges();
    }

    public int Delete(string nik)
    {
        var employee = GetById(nik);
        if (employee == null)
        {
            return 0;
        }

        _context.Set<Employee>().Remove(employee);
        return _context.SaveChanges();
    }
}
