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



            var result = new Bionuclear.Core.Models.Resultados.Resultados
            {
                comentario = request.Resultados.comentario,
                nombre_paciente = request.Resultados.nombre_paciente,
                correo_electroncio_paciente = request.Resultados.correo_electroncio_paciente,
                fecha_registro = DateTime.Now.ToString(),
                nombre_doctor = request.Resultados.nombre_doctor,
                sexo_paciente = request.Resultados.sexo_paciente
            };

            await _context.Resultados.AddAsync(result);
            await _context.SaveChangesAsync();

            return request.Resultados;
        }
    }
}
