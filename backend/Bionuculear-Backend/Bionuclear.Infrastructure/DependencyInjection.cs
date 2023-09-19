using Bionuclear.Application;
using Bionuclear.Core.AzureFiles;
using Bionuclear.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bionuclear.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQL_AZURE_DB"),
              b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IFileShare, FileShare>();

            //mediatr
            services.AddMediatR(dd => { dd.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

            return services;
        }
    }
}
