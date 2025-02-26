using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen1_LeonardoMadrigal.Models
{
    public class Boleto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int RutaId { get; set; }

        [Required]
        public int VehiculoId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        [Required]
        public int Cantidad { get; set; }

        // Relaciones con otras entidades 
        public Usuario? Usuario { get; set; }

        public Ruta? Ruta { get; set; }

        public Vehiculo? Vehiculo { get; set; }
    }
}
