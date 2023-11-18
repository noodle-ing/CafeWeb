using System.ComponentModel.DataAnnotations;
using maxutova_altynay_09.Models;

namespace maxutova_altynay_09.ViewModels.Cafe;

public class CreateCafeViewModel
{
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Required")]
    public UploadImage? CafePhoto { get; set; }
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Description")]
    public string Description { get; set; }
}