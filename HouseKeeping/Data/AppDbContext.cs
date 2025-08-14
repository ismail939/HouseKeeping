using HouseKeeping.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Housekeeper> Housekeepers { get; set; }
    public DbSet<DailyJobEntry> DailyJobEntries { get; set; }
    public DbSet<Admin> Admins { get; set; }

    // Add other DbSets for your models here
}
