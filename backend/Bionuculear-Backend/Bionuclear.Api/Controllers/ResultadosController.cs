using Bionuclear.Core.AzureFiles;
using Bionuclear.Core.Dtos;
using Bionuclear.Infrastructure.Sql.Commands.Login;
using Bionuclear.Infrastructure.Sql.Commands.Resultados;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private IMediator mediator;

        public ResultadosController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarResultados(ResultadosDtos resultados)
        {
            var datos = await mediator.Send(new ResultadosCommand(resultados));
            return Ok(datos);
        }
    }
}
