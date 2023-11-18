using maxutova_altynay_09.Context;
using maxutova_altynay_09.Extensions.Maping;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.Services;
using maxutova_altynay_09.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maxutova_altynay_09.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly CafeContext _context;
    private readonly UserManager<User> _userManager;
    private readonly AccountService _accountService;

    public AccountController(SignInManager<User> signInManager, CafeContext context, UserManager<User> userManager, AccountService accountService)
    {
        _signInManager = signInManager;
        _context = context;
        _userManager = userManager;
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationView model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (_context.Users.Any(u => u.UserName!.ToLower() == model.Login.ToLower()))
        {
            ModelState.AddModelError("", "Your login is busy");
            return View(model);
        }

        if (_context.Users.Any(u => u.Email!.ToLower() == model.Email.ToLower()))
        {
            ModelState.AddModelError("", "This email is busy");
            return View(model);
        }
        var result = await _accountService.RegistrationNewUser(model);
        
        if (!result)
            return BadRequest();
        
        return RedirectToAction("AllCafe", "Cafe");
    }
    
    [HttpGet]
    public IActionResult Login() =>
        View();
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginView model)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        User? user = await _userManager.FindByEmailAsync(model.Email) ?? await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Email);
        if (user is not null)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("AllCafe", "Cafe");
        }
        return BadRequest();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    
    [HttpGet]
    public IActionResult UserProfile()
    { 
        string id = _userManager.GetUserId(User)!;
        User user = _context.Users.Find(id);
        if (user is not null)
        {
            return View(user);
        }
        return NotFound();  
    }
    
    [HttpPost]
    public async Task<IActionResult> EditProfiles(EditProfileAllView model)
    {
        var result = false;
        if (!ModelState.IsValid)
            return Json(123);


        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
        if (user is not null)
        {
            result = true;
            user = user.EditUser(model);
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        return !result ? Json(123) : Json(model);
    }
    
}