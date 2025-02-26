namespace Examen1_LeonardoMadrigal.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int CapacidadDePasajeros { get; set; }
        public string Estado { get; set; } // (Bueno, Regular, Necesita mantenimiento)
        public DateTime FechaRegistro { get; set; }
        public int UsuarioId { get; set; } // Referencia a la tabla de usuario

        // Relacion con la tabla de usuario
        public Usuario Usuario { get; set; }

        public IEnumerable<Boleto> Boletos { get; set; }

    }
}
