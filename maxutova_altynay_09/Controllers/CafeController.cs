using maxutova_altynay_09.Context;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.Services;
using maxutova_altynay_09.ViewModels.Cafe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace maxutova_altynay_09.Controllers;

public class CafeController : Controller
{
    
    private readonly SignInManager<User> _signInManager;
    private readonly CafeContext _context;
    private readonly CafeService _cafeService;
    private readonly IHostEnvironment _environment;
    private readonly UploadFileService _fileUploadService;
    private readonly UserManager<User> _userManager;

    public CafeController(SignInManager<User> signInManager, CafeContext context, CafeService cafeService, IHostEnvironment environment, UploadFileService fileUploadService, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _context = context;
        _cafeService = cafeService;
        _environment = environment;
        _fileUploadService = fileUploadService;
        _userManager = userManager;
    }

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

    [HttpGet]
    public IActionResult Details(int cafeId)
    {
        if (cafeId.ToString() is null)
            return BadRequest();

        Cafe cafe = _context.Cafes.FirstOrDefault(c => c.Id == cafeId);
        if (cafe is not null)
        {
            List<Photo> gallery = new List<Photo>();
            gallery = _context.Photos.Where(p => p.CafeId == cafe.Id).ToList();
            cafe.Gallery = gallery;
            return View(cafe);
        }

        return BadRequest();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewReview(CreateRestView model)
    {
        if (model.Comment != null && model.CafeId !=null)
        {
            Review review = new Review
            {
                Comment = model.Comment,
                CafeId = model.CafeId,
                UserId = _userManager.GetUserId(User),
                Rate = model.Rate
            };

            if (model.Image != null)
            {
                string fileName = ContentDispositionHeaderValue.Parse(model.Image.ContentDisposition).FileName
                    .ToString().Trim();
                string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/Files");
                string newName = model.Comment + fileName;
                string path = $"/files/{newName}";
                await _fileUploadService.UploadFile(filePath, newName, model.Image);
                review.CafePhoto = path;
                Photo photo = new Photo
                {
                    PhotoPath = path,
                    CafeId = model.CafeId
                };
                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
            }
            else
            {
                string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/Files");
                string newName = "default.png";
                string path = $"/files/{newName}";
                review.CafePhoto = path;
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Json(review);
        }
        return Json(123);
    }


    [HttpGet]
    public IActionResult AllComment(int cafeId)
    {
        List<Review> reviews = new List<Review>();
        reviews = _context.Reviews.Where(r => r.CafeId == cafeId).ToList();
        return Json(reviews);
    }
    
    
    [HttpGet]
    public IActionResult AllGallery(int cafeId)
    {
        List<Photo> reviews = new List<Photo>();
        reviews = _context.Photos.Where(r => r.CafeId == cafeId).ToList();
        return Json(reviews);
    }


    [HttpGet]
    public IActionResult CafeRate(int cafeId)
    {
        List<Review> reviews = _context.Reviews.Where(r => r.CafeId == cafeId).ToList();
        int sumRev = reviews.Sum(r => r.Rate);
        int numRev = reviews.Count();
        Cafe cafe = _context.Cafes.FirstOrDefault(c => c.Id == cafeId);
        if (numRev > 0)
        {
            cafe.Rating = Math.Round((double)sumRev / numRev, 1); 
        }
        else
        {
            cafe.Rating = 0; 
        }
        double rating = cafe.Rating;
        _context.Cafes.Update(cafe);
        _context.SaveChanges();
        return Json(rating);
    }

    [HttpDelete]
    public IActionResult DeleteComment(int id)
    {
        Review? review = _context.Reviews.FirstOrDefault(r => r.Id == id);
        if (review is not null)
        {
            _context.Reviews.Remove(review);
            _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return NotFound($"User with ID {id} not found");
        }
    }
}