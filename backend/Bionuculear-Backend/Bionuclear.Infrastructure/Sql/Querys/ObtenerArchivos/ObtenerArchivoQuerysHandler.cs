using Bionuclear.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerArchivos
{
    public class ObtenerArchivoQuerysHandler : IRequestHandler<ObtenerArchivoQuerys, string>
    {
        private IApplicationDbContext _context;
        public ObtenerArchivoQuerysHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(ObtenerArchivoQuerys request, CancellationToken cancellationToken)
        {
            var documento = await _context.LinksResultados.FirstOrDefaultAsync(x => x.numero_expediente == request.expediente);
            return documento.nombre_documento;
        }
    }
}
