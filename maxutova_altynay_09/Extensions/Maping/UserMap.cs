
using maxutova_altynay_09.Context;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.ViewModels.Account;

namespace maxutova_altynay_09.Extensions.Maping;

public static class UserMap
{
    private static readonly CafeContext _context;
    
    public static User CreateUserMaping(this RegistrationView? model)
    {
        var user = model == null
            ? null
            : new User
            {
                UserName = model.Login,
                Email = model.Email,
                PhoneNumber = model.PhoneNum,
            };
        if (model!.Image is null)
            user!.Avatar = "/files/default.png";

        else
            user!.Avatar = "/files/" + model.Login + model.Image.File.FileName;

        
        return user;
    }
    
    
    public static User EditUser(this User user, EditProfileAllView model)
    {
        if (model is null)  
            return null;

        user.UserName = model.Name;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        return user;
    }
}

