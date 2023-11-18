using System.ComponentModel.DataAnnotations;

namespace maxutova_altynay_09.ViewModels.Cafe;

public class CafeFilterViewModel
{
    [Display(Name = "Filter by Name")]
    public string CafeName { get; set; }
    
    [Display(Name = "Filter by Name")]
    public string Description { get; set; }

}