using WebApp.Context;
using WebApp.Models;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories.Data;

public class EducationRepository : GeneralRepository<int, Education, MyContext>, IEducationRepository
{
    public EducationRepository(MyContext context) : base(context)
    {
    }
}
