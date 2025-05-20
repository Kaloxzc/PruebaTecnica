using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class TarifaPersonaAdicional
{
    public int IdTarifaPersonaAdicional { get; set; }

    public decimal? Precio { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<TarifaPersonaAdicionalTemporadum> TarifaPersonaAdicionalTemporada { get; set; } = new List<TarifaPersonaAdicionalTemporadum>();
}
