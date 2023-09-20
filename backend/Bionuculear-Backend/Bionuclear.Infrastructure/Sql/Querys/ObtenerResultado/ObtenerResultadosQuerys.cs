using Bionuclear.Core.Models.Resultados;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerResultado
{
    public record ObtenerResultadosQuerys(string? id) : IRequest<IEnumerable<Resultados>>;
}
