using maxutova_altynay_09.Context;
using maxutova_altynay_09.Extensions.Maping;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace maxutova_altynay_09.Services;

public class AccountService
{
    private readonly DefaultContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IHostEnvironment _environment;
    private readonly UploadFileService _uploadFile;

    public AccountService(DefaultContext context, UserManager<User> userManager, SignInManager<User> signInManager, IHostEnvironment environment, UploadFileService uploadFile)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _environment = environment;
        _uploadFile = uploadFile;
    }
    
    public async Task<bool> RegistrationNewUser(RegistrationView model)
    {
        if (model.Image is not null)
            _ = UploadFile(model);

        var user = model.CreateUserMaping();
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return true;
        }
        return false;
    }
    
    
    private async Task UploadFile(RegistrationView model)
    {
        string  newName = model.Login + model.Image.File.FileName;
        string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/files");
        await _uploadFile.UploadFile(filePath, newName, model.Image.File);
    }
}