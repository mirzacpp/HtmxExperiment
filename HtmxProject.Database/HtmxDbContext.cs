using Microsoft.EntityFrameworkCore;
using HtmxProject.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HtmxProject.Database;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CompanyNumber)
        .IsRequired()
        .HasMaxLength(20);
        builder.Property(c => c.IsVerified).IsRequired();
    }
}

public sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
        .IsRequired()
        .HasMaxLength(256);

        builder.HasOne<Company>()
        .WithMany()
        .HasForeignKey(i => i.CompanyId);
    }
}

public class HtmxDbContext : DbContext
{
    public HtmxDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HtmxDbContext).Assembly);
    }
}

public class DesignTimeFactory : IDesignTimeDbContextFactory<HtmxDbContext>
{
    public HtmxDbContext CreateDbContext(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        bool isDevelopment = environment.Equals("Development", StringComparison.OrdinalIgnoreCase);

        // Build config
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../HtmxProject"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<HtmxDbContext>();
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=HtmxDb;Username=postgres;Password=admin")
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging(isDevelopment)
            .EnableDetailedErrors(isDevelopment);

        return new HtmxDbContext(optionsBuilder.Options);
    }
}

public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddHtmxDbContext(this IServiceCollection services, bool isDevelopment)
    {
        services.AddDbContext<HtmxDbContext>(options =>
        {
            options
            .UseNpgsql("Host=localhost;Database=HtmxDb;Username=postgres;Password=admin")
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging(isDevelopment)
            .EnableDetailedErrors(isDevelopment);
        });

        return services;
    }
}
