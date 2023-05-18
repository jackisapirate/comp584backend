using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WorldModel;

public partial class WorldCitiesContext : IdentityDbContext<WorldCitiesUser>
{
    public WorldCitiesContext()
    {
    }

    public WorldCitiesContext(DbContextOptions<WorldCitiesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            //.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            ;
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Car>().ToTable("car");
        modelBuilder.Entity<Model>().ToTable("model");
        modelBuilder.Entity<Make>().ToTable("make");
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
