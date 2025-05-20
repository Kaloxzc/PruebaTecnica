using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class TarifaPersonaAdicionalTemporadum
{
    public int Id { get; set; }

    public int? IdTarifaPersonaAdicional { get; set; }

    public int? IdTemporada { get; set; }

    public decimal? Precio { get; set; }

    public virtual TarifaPersonaAdicional? IdTarifaPersonaAdicionalNavigation { get; set; }

    public virtual Temporadum? IdTemporadaNavigation { get; set; }
}
