using Bionuclear.Application;
using Bionuclear.Core.Models;
using Bionuclear.Core.Models.LinksResultados;
using Bionuclear.Core.Models.Resultados;
using Microsoft.EntityFrameworkCore;

namespace Bionuclear.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Resultados> Resultados { get; set; }
        public DbSet<LinksResultados> LinksResultados { get; set; }
        public DbSet<ColaCorreos> ColaCorreos { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
