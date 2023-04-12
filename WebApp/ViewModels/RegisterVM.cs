using WebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.ViewModels;

public class RegisterVM
{
    [Display(Name = "NIK")]
    public string NIK { get; set; }
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }
    [Display(Name = "Birth Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    [Range(0, 4, ErrorMessage = "GPA must be between {1} and {2}")]
    public string GPA { get; set; }
    [Display(Name = "University Name")]
    public string UniversityName { get; set; }
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Password must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password),ErrorMessage ="Not Match")]
    public string ConfirmPassword { get; set; }
}
