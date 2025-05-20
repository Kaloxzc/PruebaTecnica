using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models
{
    public class MisReservasApt
    {
        public int IdReserva { get; set; }

        [Required]
        [Display(Name = "Apartamento")]
        public int IdApartamento { get; set; }

        [Required]
        [Display(Name = "Tarifa")]
        public int IdTarifa { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Llegada")]
        public DateTime FechaLlegada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Salida")]
        public DateTime FechaSalida { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Estado")]

        // Propiedades de navegación
        public virtual Apartamento Apartamento { get; set; }
        public virtual TarifaApartamento Tarifa { get; set; }
    }
}
