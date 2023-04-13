using WebApp.Repositories.Contracts;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        var gender = new List<SelectListItem>{
            new SelectListItem
            {
                Value = "0",
                Text = "Male"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Female"
            }
        };
        ViewBag.Gender = gender;
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterVM registerVM)
    {
        var result = _accountRepository.Register(registerVM);

        if (result > 0)
        {
            return RedirectToAction("Login", "Account");
        } else {
            ModelState.AddModelError(string.Empty, "E-Mail or Phone Number is already exist");
        }

        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginVM loginVM)
    {
        if (_accountRepository.Login(loginVM)) {
            var userdata = _accountRepository.GetUserData(loginVM.Email);

            HttpContext.Session.SetString("email", userdata.Email);
            HttpContext.Session.SetString("fullName", userdata.FullName);
            HttpContext.Session.SetString("role", userdata.Role);

            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "E-Mail or Password is wrong");

        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index), "Home");
    }
}
