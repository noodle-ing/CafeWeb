namespace maxutova_altynay_09.Services;

public class UploadFileService
{
    public async Task UploadFile(string path, string filename, IFormFile file)
    {
        await using var strean = new FileStream(Path.Combine(path, filename), FileMode.Create);
        await file.CopyToAsync(strean);
    }
}