namespace Bionuclear.Core.Models
{
    public class ColaCorreos
    {
        public int id { get; set; }
        public string correo_electronico { get; set; }
        public bool enviado { get; set; } = false;
        public string body { get; set; }
    }
}
