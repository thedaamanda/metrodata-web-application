using WebApp.Models;
using WebApp.ViewModels;
using WebApp.Context;
using WebApp.Repositories.Contracts;

namespace WebApp.Repositories;

public class EducationRepository : IRepository<int, Education>
{
    private readonly MyContext _context;
    private readonly UniversityRepository _universityRepository;

    public EducationRepository(MyContext context, UniversityRepository universityRepository)
    {
        _context = context;
        _universityRepository = universityRepository;
    }

    public IEnumerable<Education> GetAll()
    {
        return _context.Set<Education>().ToList();
    }

    public Education? GetById(int key)
    {
        return _context.Set<Education>().Find(key);
    }

    public int Insert(Education education)
    {
        _context.Set<Education>().Add(education);
        return _context.SaveChanges();
    }

    public IEnumerable<Education> Search(string major)
    {
        return GetAll().Where(x => x.Major.Contains(major));
    }

    public int Update(Education education)
    {
        _context.Set<Education>().Update(education);
        return _context.SaveChanges();
    }

    public int Delete(int key)
    {
        var education = GetById(key);
        if (education == null)
        {
            return 0;
        }

        _context.Set<Education>().Remove(education);
        return _context.SaveChanges();
    }

    public IEnumerable<EducationUniversityVM> GetEducationUniversities()
    {
        var educationUniversityVM = from education in GetAll()
                                    join university in _universityRepository.GetAll() on education.UniversityId equals university.Id
                                    select new EducationUniversityVM
                                    {
                                        Id = education.Id,
                                        Major = education.Major,
                                        Degree = education.Degree,
                                        GPA = education.GPA,
                                        UniversityName = university.Name
                                    };
        return educationUniversityVM.ToList();
    }

    public EducationUniversityVM GetByIdEducations(int key)
    {
        var education = GetById(key);
        var university = new EducationUniversityVM
        {
            Id = education.Id,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.GPA,
            UniversityName = _universityRepository.GetById(education.UniversityId).Name
        };

        return university;
    }
}
