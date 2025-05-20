using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models;

public partial class Alojamiento
{
    public int IdAlojamiento { get; set; }

    public int? IdSede { get; set; }

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public int? NumeroHabitaciones { get; set; }

    public int? NumeroBanos { get; set; }

    public int? CapacidadMaxima { get; set; }

    public bool? Sala { get; set; }

    public bool? Cocina { get; set; }

    public bool? Cocineta { get; set; }

    public bool? Terraza { get; set; }

    public bool? Comedor { get; set; }

    public bool? Televisor { get; set; }

    public bool? SofaCama { get; set; }

    public bool? Nevera { get; set; }

    public string? Observaciones { get; set; }

    public byte[]? ImagenAlojamiento { get; set; }

    public virtual ICollection<Habitacion> Habitacions { get; set; } = new List<Habitacion>();

    public virtual Sede? IdSedeNavigation { get; set; }

    public virtual ICollection<TarifaAlojamiento> TarifaAlojamientos { get; set; } = new List<TarifaAlojamiento>();
    [NotMapped] 
    public string? ImagenDataURL { get; set; }
}
