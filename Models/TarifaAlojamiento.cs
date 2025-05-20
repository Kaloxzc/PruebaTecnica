using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class TarifaAlojamiento
{
    public int IdTarifa { get; set; }

    public int? IdAlojamiento { get; set; }

    public int? IdTemporada { get; set; }

    public int? NumeroHabitacion { get; set; }

    public decimal? Precio { get; set; }

    public int? PersonasIncluidas { get; set; }

    public virtual Alojamiento? IdAlojamientoNavigation { get; set; }

    public virtual Temporadum? IdTemporadaNavigation { get; set; }
}
