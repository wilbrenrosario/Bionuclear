using Bionuclear.Application;
using Bionuclear.Core.Models;
using MediatR;

namespace Bionuclear.Infrastructure.Sql.Commands.Update
{
    public class UpdateResultadosCommandHandler : IRequestHandler<UpdateResultadosCommand>
    {
        private IApplicationDbContext _context;
        public UpdateResultadosCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateResultadosCommand request, CancellationToken cancellationToken)
        {
            var resultado = _context.Resultados.FirstOrDefault(x => x.id == request.resultados.id);

            resultado.comentario = request.resultados.comentario;
            resultado.nombre_paciente = request.resultados.nombre_paciente;
            resultado.correo_electroncio_paciente = request.resultados.correo_electroncio_paciente;
            resultado.nombre_doctor = request.resultados.nombre_doctor;
            resultado.sexo_paciente = request.resultados.sexo_paciente;

            resultado.numero_expediente = request.resultados.numero_expediente == "0" ? resultado.numero_expediente : request.resultados.numero_expediente;

            await _context.SaveChangesAsync();
        }
    }
}
