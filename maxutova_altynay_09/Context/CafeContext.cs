using maxutova_altynay_09.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace maxutova_altynay_09.Context;

public class CafeContext : IdentityDbContext<User> 
{
    public DbSet<User> Users { get; set; }
    public DbSet<Cafe> Cafes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public CafeContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}