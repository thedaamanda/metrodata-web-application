using WebApp.Context;
using WebApp.Models;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories.Data;

public class EmployeeRepository : GeneralRepository<string, Employee, MyContext>, IEmployeeRepository
{
    public EmployeeRepository(MyContext context) : base(context)
    {
    }
}
