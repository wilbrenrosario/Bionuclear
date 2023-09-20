using Bionuclear.Core.Models.Resultados;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerResultado
{
    public record ObtenerResultadosQuerys : IRequest<IEnumerable<Resultados>>;
}
