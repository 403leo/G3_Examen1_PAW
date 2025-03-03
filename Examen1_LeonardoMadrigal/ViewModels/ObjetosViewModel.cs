using Examen1_LeonardoMadrigal.Models;

namespace Examen1_LeonardoMadrigal.ViewModels
{
    public class ObjetosViewModel
    {
        public IEnumerable<Vehiculo> Vehiculos { get; set; }
        public IEnumerable<Ruta> Rutas { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Boleto> Boleto { get; set; }


    }
}
