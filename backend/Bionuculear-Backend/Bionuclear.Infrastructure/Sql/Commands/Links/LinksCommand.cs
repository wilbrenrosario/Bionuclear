using Bionuclear.Core.Dtos;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.LinksResultados
{
    public record LinksCommand(LinksDtos Links) : IRequest<String>;
}
