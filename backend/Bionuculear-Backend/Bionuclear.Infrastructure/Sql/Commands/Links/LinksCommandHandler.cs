using Bionuclear.Application;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.LinksResultados
{
    public class LinksCommandHandler : IRequestHandler<LinksCommand, String>
    {
        private IApplicationDbContext _context;
        public LinksCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(LinksCommand request, CancellationToken cancellationToken)
        {
            var expediente = "Expediente-" + Guid.NewGuid().ToString();

            var result = new Bionuclear.Core.Models.LinksResultados.LinksResultados
            {
                nombre_documento = request.Links.nombre_documento,
                numero_expediente = expediente,
            };

            await _context.LinksResultados.AddAsync(result);
            await _context.SaveChangesAsync();

            return expediente;
        }
    }
}
