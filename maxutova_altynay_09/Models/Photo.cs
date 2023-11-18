namespace maxutova_altynay_09.Models;

public class Photo
{
    public int Id { get; set; }
    public string PhotoPath { get; set; }
    public int CafeId { get; set; }
    public Cafe? Cafe { get; set; }
}