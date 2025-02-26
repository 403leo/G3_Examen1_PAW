namespace Examen1_LeonardoMadrigal.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        // Relaciones con las tablas
        public IEnumerable<Ruta> Rutas { get; set; }
        public IEnumerable<Vehiculo> Vehiculos { get; set; }
        public IEnumerable<Boleto> Boletos { get; set; }

    }
}
