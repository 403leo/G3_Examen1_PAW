using Examen1_LeonardoMadrigal.Models;

namespace Examen1_LeonardoMadrigal.ViewModels
{
    public class VehiculosRutasViewModel
    {
        public IEnumerable<Vehiculo> Vehiculos { get; set; }
        public IEnumerable<Ruta> Rutas { get; set; }
        public IEnumerable<Boleto> Boletos { get; set; }

    }
}
