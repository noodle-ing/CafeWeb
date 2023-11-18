using maxutova_altynay_09.Context;
using maxutova_altynay_09.Models;
using maxutova_altynay_09.ViewModels.Cafe;

namespace maxutova_altynay_09.Extensions.Maping;

public static class CafeMap
{
    private static readonly CafeContext _context;
    
    public static Cafe CreateCafeMaping(this CreateCafeViewModel? model)
    {
        var cafe = model == null
            ? null
            : new Cafe
            {
                Name = model.Name,
                Description = model.Description,
                Rating = 0
            };
        if (model!.CafePhoto is null)
            cafe!.CafePhoto = "/files/default.png";

        else
            cafe!.CafePhoto = "/files/" + model.Name + model.CafePhoto.File.FileName;

        
        return cafe;
    }
    
    
}