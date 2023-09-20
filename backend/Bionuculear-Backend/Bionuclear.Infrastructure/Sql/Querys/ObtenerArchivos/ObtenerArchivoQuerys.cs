using MediatR;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerArchivos
{
    public record ObtenerArchivoQuerys(string expediente) : IRequest<string>;
}
