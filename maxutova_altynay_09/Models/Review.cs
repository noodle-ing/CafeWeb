namespace maxutova_altynay_09.Models;

public class Review
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public int NumRewiew { get; set; }
    public string? CafePhoto { get; set; }   
    public string UserId { get; set; }
    public int Rate { get; set; }
    public int CafeId { get; set; }
    public Cafe? Cafe { get; set; }
    public User? User { get; set;}
}