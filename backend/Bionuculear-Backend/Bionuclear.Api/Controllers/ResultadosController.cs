using Bionuclear.Core.AzureFiles;
using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models.Resultados;
using Bionuclear.Infrastructure.Sql.Commands.Login;
using Bionuclear.Infrastructure.Sql.Commands.Resultados;
using Bionuclear.Infrastructure.Sql.Commands.Update;
using Bionuclear.Infrastructure.Sql.Querys.ObtenerMisResultados;
using Bionuclear.Infrastructure.Sql.Querys.ObtenerResultado;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private IMediator mediator;

        public ResultadosController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerResultados()
        {
            var datos = await mediator.Send(new ObtenerResultadosQuerys("-1"));
            return Ok(datos);
        }

        [HttpGet("Me")]
        public async Task<IActionResult> ObtenerMisResultados(string correo)
        {
            var datos = await mediator.Send(new MisResultadosQuerys(correo));
            return Ok(datos);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> ObtenerResultadosById(string id)
        {
            var datos = await mediator.Send(new ObtenerResultadosQuerys(id));
            return Ok(datos);
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarResultados(ResultadosDtos resultados)
        {
            var datos = await mediator.Send(new ResultadosCommand(resultados));
            return Ok(datos);
        }

        [HttpPost("updateresultados")]
        public async Task<IActionResult> UpdateResultados(Resultados resultados)
        {
            await mediator.Send(new UpdateResultadosCommand(resultados));
            return Ok();
        }
    }
}
