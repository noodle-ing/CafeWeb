using maxutova_altynay_09.Context;
using maxutova_altynay_09.Extensions.Maping;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.ViewModels.Cafe;
using Microsoft.AspNetCore.Identity;

namespace maxutova_altynay_09.Services;

public class CafeService
{
    private readonly CafeContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IHostEnvironment _environment;
    private readonly UploadFileService _uploadFile;


    public CafeService(CafeContext context, UserManager<User> userManager, SignInManager<User> signInManager, IHostEnvironment environment, UploadFileService uploadFile)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _environment = environment;
        _uploadFile = uploadFile;
    }

    public async Task<bool> RegistrationNewCafe(CreateCafeViewModel model)
    {
        if (model.CafePhoto is not null)
            _ = UploadFile(model);

        var cafe = model.CreateCafeMaping();
        
        if (cafe is not null)
        {
            _context.Cafes.Add(cafe);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    
    
    private async Task UploadFile(CreateCafeViewModel model)
    {
        string  newName = model.Name + model.CafePhoto.File.FileName;
        string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/files");
        await _uploadFile.UploadFile(filePath, newName, model.CafePhoto.File);
    }
}