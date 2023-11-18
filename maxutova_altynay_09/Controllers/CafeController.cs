using maxutova_altynay_09.Context;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.Services;
using maxutova_altynay_09.ViewModels.Cafe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maxutova_altynay_09.Controllers;

public class CafeController : Controller
{
    
    private readonly SignInManager<User> _signInManager;
    private readonly CafeContext _context;
    private readonly CafeService _cafeService;

    public CafeController(SignInManager<User> signInManager, CafeContext context, CafeService cafeService, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _context = context;
        _cafeService = cafeService;
        _userManager = userManager;
    }

    private readonly UserManager<User> _userManager;
    
    [HttpGet]
    public IActionResult CreteCafe()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreteCafe(CreateCafeViewModel model)
    {
        if (!_signInManager.IsSignedIn(User))
            return BadRequest();

        if (!ModelState.IsValid)
            return View(model);
        
        
        if (_context.Cafes.Any(u => u.Name!.ToLower() == model.Name.ToLower()))
        {
            ModelState.AddModelError("", $"Your input name:{model.Name} is already taken");
            return View(model);
        }
        
        var result = await _cafeService.RegistrationNewCafe(model);
        
        if (!result)
            return BadRequest();
        
        return RedirectToAction("AllCafe", "Cafe");
    }

    [HttpGet]
    public async Task<IActionResult> AllCafe(int page = 1)
    {
        int pageSize = 10;
        IQueryable<Cafe> cafes = _context.Cafes;
        var count = await cafes.CountAsync();
        var items = await cafes.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        CafeIndexViewModel viewModel = new CafeIndexViewModel
        {
            PageViewModel = pageViewModel,
            Cafes = items
        };
        
        return View(viewModel);
    }
}