namespace Bionuclear.Core.Models
{
    public class Usuarios
    {
        public int id { get; set; }
        public string usuario { get; set; } //admin001 * user001
        public string clave { get; set; }
        public string nombre_completo {  get; set; }
        public int tipo_usuario { get; set; }
        public string correo_electronico { get; set; }
    }
}
