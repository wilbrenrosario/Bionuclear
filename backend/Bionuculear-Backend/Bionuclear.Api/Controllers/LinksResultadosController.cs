using Bionuclear.Infrastructure.Sql.Querys.ObtenerArchivos;
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
    public class LinksResultadosController : ControllerBase
    {
        private IMediator mediator;

        public LinksResultadosController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerArchivoNombre(string expdiente)
        {
            var datos = await mediator.Send(new ObtenerArchivoQuerys(expdiente));
            return Ok(datos);
        }
    }
}
