using Bionuclear.Core.Dtos;
using Bionuclear.Infrastructure.Sql.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Login() { 

            return Ok("HOLA");
        }
    }
}
