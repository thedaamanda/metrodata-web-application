using WebApp.Models;

namespace WebApp.Repositories.Contracts;

public interface IUniversityRepository
{
    IEnumerable<University> GetAll();
    University? GetById(int id);
    IEnumerable<University> Search(string name);
    int Insert(University university);
    int Update(University university);
    int Delete(int id);
}
