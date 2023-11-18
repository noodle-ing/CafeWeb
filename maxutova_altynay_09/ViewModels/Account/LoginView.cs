using System.ComponentModel.DataAnnotations;

namespace maxutova_altynay_09.ViewModels.Account;

public class LoginView
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    public string? LoginError { get; set; }
    public string? ReturnUrl { get; set; }
}