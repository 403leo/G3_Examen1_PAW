namespace Examen1_LeonardoMadrigal.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<string> Paradas { get; set; } = new List<string>();
        public List<DateTime> Horarios { get; set; } = new List<DateTime>();
        public string Estado { get; set; } // Activo/Inactivo
        public DateTime FechaRegistro { get; set; }
        public int UsuarioId { get; set; } // Referencia a la tabla de usuario

        // Referencia a la tabla de usuario
        public Usuario? Usuario { get; set; }

        public IEnumerable<Boleto>? Boletos { get; set; }

    }
}
