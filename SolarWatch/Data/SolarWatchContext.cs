using Microsoft.EntityFrameworkCore;

namespace SolarWatch.Data;

public class SolarWatchContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunsetTimes> SunriseSunsetTimes { get; set; }
    
    public SolarWatchContext(DbContextOptions<SolarWatchContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1435;Database=focused_beaver;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;");
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<City>()
            .HasIndex(u => u.Name)
            .IsUnique();
        
        builder.Entity<City>()
            .HasIndex(u => u.Id)
            .IsUnique();

        builder.Entity<SunriseSunsetTimes>()
            .HasIndex(u => u.Id)
            .IsUnique();
    }
}