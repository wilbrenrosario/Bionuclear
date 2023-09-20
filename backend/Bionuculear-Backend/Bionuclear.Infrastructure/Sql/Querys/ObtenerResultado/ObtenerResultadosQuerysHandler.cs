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
            if (request.id != "-1")
            {
                return await _context.Resultados.Where(x => x.id == int.Parse(request.id)).ToListAsync();
            }
            return await _context.Resultados.ToListAsync();
        }
    }
}
