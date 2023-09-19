namespace Bionuclear.Core.Models.Resultados
{
    public class Resultados
    {
        public int id { get; set; }
        public string comentario { get; set;}
        public string nombre_paciente { get; set;}
        public string correo_electroncio_paciente { get; set;}
        public string fecha_registro { get; set;}
        public string nombre_doctor { get; set;}
        public string sexo_paciente { get; set; }
        public string numero_expediente { get; set;}
    }
}
