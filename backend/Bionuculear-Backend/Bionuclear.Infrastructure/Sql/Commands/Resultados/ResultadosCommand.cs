using Bionuclear.Core.Dtos;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.Resultados
{
    public record ResultadosCommand(ResultadosDtos Resultados) : IRequest<ResultadosDtos>;
}
