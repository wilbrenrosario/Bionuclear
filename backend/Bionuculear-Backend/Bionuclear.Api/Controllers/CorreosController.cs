using Bionuclear.Infrastructure.Sql.Commands.Correos;
using Bionuclear.Infrastructure.Sql.Querys.ObtenerResultado;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreosController : ControllerBase
    {
        private IMediator mediator;

        public CorreosController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> buscarCorreos()
        {
            await mediator.Send(new CorreosMasivosCommand());
            return Ok();
        }
    }
}
