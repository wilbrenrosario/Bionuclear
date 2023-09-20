using Bionuclear.Application;
using Bionuclear.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bionuclear.Infrastructure.Sql.Commands.Correos
{
    public class CorreosMasivosCommandHandler : IRequestHandler<CorreosMasivosCommand>
    {
        private IApplicationDbContext _context;
        private IConfiguration configuration;
        public CorreosMasivosCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        public async Task Handle(CorreosMasivosCommand request, CancellationToken cancellationToken)
        {
            var listado_correos = _context.ColaCorreos.Where(x => x.enviado == false);
            foreach (var item in listado_correos)
            {
                if (!item.enviado)
                {
                    Correo.enviar_correo(configuration.GetSection("Email:Host").Value, int.Parse(configuration.GetSection("Email:Port").Value), configuration.GetSection("Email:UserName").Value, configuration.GetSection("Email:PassWord").Value, item.correo_electronico, item.body);
                    item.enviado = true;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
