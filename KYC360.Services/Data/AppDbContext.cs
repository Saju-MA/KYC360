using KYC360.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace KYC360.Services.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<SecretUrl>SecretUrl { get; set; }
}
