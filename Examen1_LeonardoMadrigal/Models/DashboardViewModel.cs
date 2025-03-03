namespace Examen1_LeonardoMadrigal.Models
{
    public class DashboardViewModel
    {
        // Resumen de estadísticas
        public int TotalRutasActivas { get; set; }
        public int TotalVehiculosEnBuenEstado { get; set; }
        public int TotalBoletosVendidosMesActual { get; set; }

        // Usuarios con boletos por ruta y horario
        public List<UsuarioBoletosViewModel> UsuariosConBoletos { get; set; }

    }
}
