using Bionuclear.Application;
using Bionuclear.Core.Models.Resultados;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerResultado
{
    public class ObtenerResultadosQuerysHandler : IRequestHandler<ObtenerResultadosQuerys, IEnumerable<Resultados>>
    {

        private IApplicationDbContext _context;
        public ObtenerResultadosQuerysHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Resultados>> Handle(ObtenerResultadosQuerys request, CancellationToken cancellationToken)
        {
           return await _context.Resultados.ToListAsync();
        }
    }
}
