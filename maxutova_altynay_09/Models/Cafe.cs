namespace maxutova_altynay_09.Models;

public class Cafe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CafePhoto { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public List<Photo>? Gallery { get; set; } = new();
    public List<Review>? Reviews { get; set; } = new();
}