using Bionuclear.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bionuclear.Application
{
    public interface IApplicationDbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Resultados> Resultados {  get; set; }
        public DbSet<LinksResultados> LinksResultados { get; set; }
        public DbSet<ColaCorreos> ColaCorreos { get; set; }
        Task<int> SaveChangesAsync();
    }
}
