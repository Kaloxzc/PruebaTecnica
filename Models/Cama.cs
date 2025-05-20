using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class Cama
{
    public int IdCama { get; set; }

    public int? IdHabitacion { get; set; }

    public string? Tipo { get; set; }

    public int? Cantidad { get; set; }

    public virtual Habitacion? IdHabitacionNavigation { get; set; }
}
