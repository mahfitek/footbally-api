using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Footbally.Infrastructure.Data;

public class FootballyDbContextFactory : IDesignTimeDbContextFactory<FootballyDbContext>
{
    public FootballyDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Footbally.API"))
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<FootballyDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new FootballyDbContext(optionsBuilder.Options);
    }
}