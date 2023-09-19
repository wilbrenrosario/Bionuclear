using Bionuclear.Core.AzureFiles;
using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using Bionuclear.Infrastructure.Sql.Commands.Login;
using Bionuclear.Infrastructure.Sql.Commands.Registrar;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IMediator mediator;
        public UsuariosController(IMediator _mediator) 
        {
            mediator = _mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuariosDtos usuarios)
        {
            var datos = await mediator.Send(new LoginCommand(usuarios));
            return Ok(datos);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarDtos registrar)
        {
            await mediator.Send(new RegistrarCommand(registrar));
            return Ok();
        }
    }
}
