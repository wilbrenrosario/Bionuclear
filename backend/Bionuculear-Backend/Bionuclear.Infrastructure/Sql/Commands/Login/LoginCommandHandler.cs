using Bionuclear.Application;
using Bionuclear.Core.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Bionuclear.Infrastructure.Sql.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Usuarios>
    {
        private IApplicationDbContext _context;
        public LoginCommandHandler(IApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Usuarios> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var usuario = _context.Usuarios.Where(x => x.usuario == request.Usuarios.usuario && x.clave == request.Usuarios.clave).FirstOrDefault();
            
            if (usuario == null)
            {
                throw new NotImplementedException();
            }

            return usuario;
            
        }
    }
}
