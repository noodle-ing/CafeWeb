using maxutova_altynay_09.Models;

namespace maxutova_altynay_09.ViewModels.Cafe;

public class CreateCafeViewModel
{
    public string Name { get; set; }
    public UploadImage? CafePhoto { get; set; }
    public string Description { get; set; }
}