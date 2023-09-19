using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.Registrar
{
    public record RegistrarCommand (RegistrarDtos Usuarios) : IRequest;
}
