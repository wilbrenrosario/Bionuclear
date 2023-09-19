using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.Login
{
    public record LoginCommand(UsuariosDtos Usuarios) : IRequest<Usuarios>;
}
