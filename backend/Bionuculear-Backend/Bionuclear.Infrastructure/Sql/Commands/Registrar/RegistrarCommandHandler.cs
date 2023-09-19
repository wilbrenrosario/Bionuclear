using Bionuclear.Application;
using Bionuclear.Core.Models;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.Registrar
{
    public class RegistrarCommandHandler : IRequestHandler<RegistrarCommand>
    {
        private IApplicationDbContext _context;
        public RegistrarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(RegistrarCommand request, CancellationToken cancellationToken)
        {
            var new_user = new Usuarios {
                correo_electronico = request.Usuarios.correo_electronico,
                clave = request.Usuarios.clave,
                nombre_completo = request.Usuarios.nombre_completo,
                tipo_usuario = 0
            };

            await _context.Usuarios.AddAsync(new_user);
            await _context.SaveChangesAsync();
        }
    }
}
