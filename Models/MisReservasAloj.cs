using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models
{
    public class MisReservasAloj
    {
        [Key]
        public int IdReserva { get; set; }

        [Required]
        [Display(Name = "Alojamiento")]
        public int IdAlojamiento { get; set; }

        [Required]
        [Display(Name = "Tarifa")]
        public int IdTarifa { get; set; }

        [Required]
        [Display(Name = "Temporada")]
        public int IdTemporada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Llegada")]
        [CustomValidation(typeof(MisReservasAloj), "ValidateFechasTemporada4")]
        public DateTime FechaLlegada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Salida")]
        [CustomValidation(typeof(MisReservasAloj), "ValidateFechasTemporada4")]
        public DateTime FechaSalida { get; set; }

        public static ValidationResult ValidateFechasTemporada4(DateTime fecha, ValidationContext context)
        {
            var instance = (MisReservasAloj)context.ObjectInstance;

            if (instance.IdTemporada == 4)
            {
                if (fecha.DayOfWeek == DayOfWeek.Friday ||
                    fecha.DayOfWeek == DayOfWeek.Saturday ||
                    fecha.DayOfWeek == DayOfWeek.Sunday)
                {
                    return new ValidationResult("Para la temporada 4 solo se permiten reservas de Lunes a Jueves");
                }
            }
            return ValidationResult.Success;
        }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 20)]
        [Display(Name = "Número de Personas")]
        public int NumeroPersonas { get; set; }

        [Display(Name = "Incluye Servicios")]
        public bool IncluyeServicios { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdAlojamiento")]
        public virtual Alojamiento Alojamiento { get; set; }

        [ForeignKey("IdTarifa")]
        public virtual TarifaAlojamiento Tarifa { get; set; }
        [ForeignKey("IdTemporada")]
        public virtual Temporadum Temporada { get; set; }
    }
}

