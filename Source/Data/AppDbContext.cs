using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Source.Models;
namespace Source.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }
  public DbSet<DriveFile> Files { get; set; }
}

