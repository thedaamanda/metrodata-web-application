using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class LoginVM
{
    [Display(Name = "E-Mail")]
    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
