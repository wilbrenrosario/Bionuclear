using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.Update
{
    public record UpdateResultadosCommand (Bionuclear.Core.Models.Resultados.Resultados resultados) : IRequest;
}
