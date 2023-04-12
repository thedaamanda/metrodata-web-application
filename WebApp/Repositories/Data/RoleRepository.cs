using WebApp.Context;
using WebApp.Models;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories.Data;

public class RoleRepository : GeneralRepository<int, Role, MyContext>, IRoleRepository
{
    public RoleRepository(MyContext context) : base(context)
    {
    }
}
