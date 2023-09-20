﻿using Bionuclear.Core.Models.Resultados;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Querys.ObtenerMisResultados
{
    public record MisResultadosQuerys(string id) : IRequest<IEnumerable<Resultados>>;
}
