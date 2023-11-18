using Microsoft.AspNetCore.Identity;

namespace maxutova_altynay_09.Models;

public class User : IdentityUser
{
    public string Avatar { get; set; }

}