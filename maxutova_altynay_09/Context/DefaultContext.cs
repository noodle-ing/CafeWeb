using maxutova_altynay_09.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace maxutova_altynay_09.Context;

public class DefaultContext : IdentityDbContext<User> 
{
    public DbSet<User> Users { get; set; }

    public DefaultContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}