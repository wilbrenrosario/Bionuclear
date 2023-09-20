using Bionuclear.Application;
using Bionuclear.Core.Models.Resultados;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerMisResultados
{
    public class MisResultadosQuerysHandler : IRequestHandler<MisResultadosQuerys, IEnumerable<Resultados>>
    {
        private IApplicationDbContext _context;
        public MisResultadosQuerysHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Resultados>> Handle(MisResultadosQuerys request, CancellationToken cancellationToken)
        {
            return await _context.Resultados.Where(x => x.correo_electroncio_paciente == request.correo).ToListAsync();
        }
    }
}
