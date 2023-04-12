using WebApp.Models;
using WebApp.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class RoleController : Controller
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var universities = _roleRepository.GetAll();
        return View(universities);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var role = _roleRepository.GetById(id);
        return View(role);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Role role)
    {
        _roleRepository.Insert(role);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var entity = _roleRepository.GetById(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role role)
    {
        _roleRepository.Update(role);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var entity = _roleRepository.GetById(id);
        return View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        _roleRepository.Delete(id);
        return RedirectToAction("Index");
    }
}
