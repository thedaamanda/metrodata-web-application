using WebApp.Models;
using WebApp.ViewModels;
using WebApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers;

public class EducationController : Controller
{
    private readonly EducationRepository _educationRepository;
    private readonly UniversityRepository _universityRepository;

    public EducationController(EducationRepository educationRepository, UniversityRepository universityRepository)
    {
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var universities = _educationRepository.GetEducationUniversities();
        return View(universities);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var education = _educationRepository.GetByIdEducations(id);
        return View(education);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var universities = _universityRepository.GetAll()
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        ViewBag.University = universities;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationUniversityVM educations)
    {
        _educationRepository.Insert(new Education
        {
            Id = educations.Id,
            Major = educations.Major,
            Degree = educations.Degree,
            GPA = educations.GPA,
            UniversityId = Convert.ToInt32(educations.UniversityName)
        });
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var entity = _educationRepository.GetByIdEducations(id);
        var universities = _universityRepository.GetAll()
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        ViewBag.University = universities;

        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationUniversityVM educations)
    {
        _educationRepository.Update(new Education
        {
            Id = educations.Id,
            Major = educations.Major,
            Degree = educations.Degree,
            GPA = educations.GPA,
            UniversityId = Convert.ToInt32(educations.UniversityName)
        });
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var entity = _educationRepository.GetById(id);
        return View(new EducationUniversityVM
        {
            Id = entity.Id,
            Major = entity.Major,
            Degree = entity.Degree,
            GPA = entity.GPA,
            UniversityName = _universityRepository.GetById(entity.UniversityId).Name
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        _educationRepository.Delete(id);
        return RedirectToAction("Index");
    }
}
