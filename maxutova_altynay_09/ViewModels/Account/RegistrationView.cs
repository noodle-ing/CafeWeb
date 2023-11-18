using System.ComponentModel.DataAnnotations;
using maxutova_altynay_09.Models;

namespace maxutova_altynay_09.ViewModels.Account;

public class RegistrationView
{
    
    [Required(ErrorMessage = "Required")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Required")]
    public string Email { get; set; }

    public string? EmailError { get; set; }
    public string? LoginError { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password mismatch")]
    public string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "Required")]
    public string? PhoneNum { get; set; }
    public UploadImage? Image { get; set; }
}