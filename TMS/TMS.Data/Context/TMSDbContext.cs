using Microsoft.EntityFrameworkCore;
using TMS.Core.Abstracts;
using TMS.Models.Entites;

namespace TMS.Data.Context;

public class TMSDbContext : DbContext, IDbContext
{
    public TMSDbContext(DbContextOptions options) : base(options)
    {
        if (!Database.CanConnect())
        {
            Database.EnsureCreated();
        }
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
}
