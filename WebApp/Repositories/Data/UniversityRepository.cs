using WebApp.Context;
using WebApp.Models;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories.Data;

public class UniversityRepository : GeneralRepository<int, University, MyContext>, IUniversityRepository
{
    public UniversityRepository(MyContext context) : base(context)
    {
    }
}
