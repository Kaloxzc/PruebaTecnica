using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class Habitacion
{
    public int IdHabitacion { get; set; }

    public int? IdAlojamiento { get; set; }

    public string? Nombre { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<Cama> Camas { get; set; } = new List<Cama>();

    public virtual Alojamiento? IdAlojamientoNavigation { get; set; }
}
