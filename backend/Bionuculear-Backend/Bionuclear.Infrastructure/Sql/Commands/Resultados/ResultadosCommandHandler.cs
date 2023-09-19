using Bionuclear.Application;
using Bionuclear.Core.Dtos;
using Bionuclear.Core.Models;
using Bionuclear.Infrastructure.Sql.Commands.Resultados;
using MediatR;
using Bionuclear.Core.Models.Resultados;

namespace Bionuclear.Infrastructure.Sql.Commands.Resultado
{
    public class ResultadosCommandHandler : IRequestHandler<ResultadosCommand, ResultadosDtos>
    {
        private IApplicationDbContext _context;
        public ResultadosCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResultadosDtos> Handle(ResultadosCommand request, CancellationToken cancellationToken)
        {
            var usuario = _context.Usuarios.Where(x => x.correo_electronico == request.Resultados.correo_electroncio_paciente).FirstOrDefault();
            
            if (usuario == null)
            {
                var new_user = new Usuarios
                {
                    usuario = "user" + Guid.NewGuid().ToString(),
                    clave = "123456",
                    nombre_completo = request.Resultados.nombre_paciente,
                    correo_electronico = request.Resultados.correo_electroncio_paciente,
                    tipo_usuario = 1 //Usuario cliente
                };
                await _context.Usuarios.AddAsync(new_user);
                await _context.ColaCorreos.AddAsync(new ColaCorreos { correo_electronico = request.Resultados.correo_electroncio_paciente });
            }
            else
            {
                await _context.ColaCorreos.AddAsync(new ColaCorreos { correo_electronico = request.Resultados.correo_electroncio_paciente });
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
        }
    }
}
