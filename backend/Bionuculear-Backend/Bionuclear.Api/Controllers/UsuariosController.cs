using Bionuclear.Core.AzureFiles;
using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using Bionuclear.Infrastructure;
using Bionuclear.Infrastructure.Sql.Commands.Login;
using Bionuclear.Infrastructure.Sql.Commands.Registrar;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bionuclear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IMediator mediator;
        private readonly IConfiguration _configuration;
        public UsuariosController(IMediator _mediator, IConfiguration configuration) 
        {
            mediator = _mediator;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuariosDtos usuarios)
        {
            var datos = await mediator.Send(new LoginCommand(usuarios));
            if (datos == null)
            {
                return BadRequest("Usuario no encontrado.");
            }
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var issuer = jwt.Issuer;
            var audience = jwt.Audience;
            var key = Encoding.ASCII.GetBytes
            (jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, "will"),
                new Claim(JwtRegisteredClaimNames.Email, "will@"),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return Ok(new { token = stringToken, tipo = datos.tipo_usuario, id = datos.id });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarDtos registrar)
        {
            await mediator.Send(new RegistrarCommand(registrar));
            return Ok();
        }
    }
}
