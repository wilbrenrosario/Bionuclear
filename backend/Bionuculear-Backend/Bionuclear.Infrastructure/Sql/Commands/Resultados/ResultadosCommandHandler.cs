using Bionuclear.Application;
using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using Bionuclear.Infrastructure.Sql.Commands.Resultados;
using MediatR;
using Bionuclear.Core.Models.Resultados;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Bionuclear.Infrastructure.Persistence;

namespace Bionuclear.Infrastructure.Sql.Commands.Resultado
{
    public class ResultadosCommandHandler : IRequestHandler<ResultadosCommand, ResultadosDtos>
    {
        private IApplicationDbContext _context;
        private IConfiguration configuration;
        public ResultadosCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        public async Task<ResultadosDtos> Handle(ResultadosCommand request, CancellationToken cancellationToken)
        {
            var usuario = _context.Usuarios.Where(x => x.correo_electronico == request.Resultados.correo_electroncio_paciente).FirstOrDefault();
            
            if (usuario == null)
            {
                var new_user = new Usuarios
                { clave = "123456",
                    nombre_completo = request.Resultados.nombre_paciente,
                    correo_electronico = request.Resultados.correo_electroncio_paciente,
                    tipo_usuario = 1 //Usuario cliente
                };
                await _context.Usuarios.AddAsync(new_user);
                await _context.ColaCorreos.AddAsync(new ColaCorreos { correo_electronico = request.Resultados.correo_electroncio_paciente });
                var body = "<b>Usuario<b>: " + request.Resultados.correo_electroncio_paciente + " <br> <b>Clave<b>: 123456 <br><br> PORTAL WEB: https://master--incandescent-sunburst-c9c837.netlify.app";
                Correo.enviar_correo(configuration.GetSection("Email:Host").Value, int.Parse(configuration.GetSection("Email:Port").Value), configuration.GetSection("Email:UserName").Value, configuration.GetSection("Email:PassWord").Value, request.Resultados.correo_electroncio_paciente, body);
            }
            else
            {
                await _context.ColaCorreos.AddAsync(new ColaCorreos { correo_electronico = request.Resultados.correo_electroncio_paciente });
                var body = "Sus resultados estan listos, favor dirigirse a la web.  <br><br> PORTAL WEB: https://master--incandescent-sunburst-c9c837.netlify.app";
                Correo.enviar_correo(configuration.GetSection("Email:Host").Value, int.Parse(configuration.GetSection("Email:Port").Value), configuration.GetSection("Email:UserName").Value, configuration.GetSection("Email:PassWord").Value, request.Resultados.correo_electroncio_paciente, body);
            }

            var result = new Bionuclear.Core.Models.Resultados.Resultados
            {
                comentario = request.Resultados.comentario,
                nombre_paciente = request.Resultados.nombre_paciente,
                correo_electroncio_paciente = request.Resultados.correo_electroncio_paciente,
                fecha_registro = DateTime.Now.ToString(),
                nombre_doctor = request.Resultados.nombre_doctor,
                sexo_paciente = request.Resultados.sexo_paciente,
                numero_expediente = request.Resultados.numero_expediente
            };

            await _context.Resultados.AddAsync(result);

            await _context.SaveChangesAsync();

            return request.Resultados;
        }    }
}
