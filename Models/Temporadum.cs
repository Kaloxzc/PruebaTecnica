using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class Temporadum
{
    public int IdTemporada { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<TarifaAlojamiento> TarifaAlojamientos { get; set; } = new List<TarifaAlojamiento>();

    public virtual ICollection<TarifaApartamento> TarifaApartamentos { get; set; } = new List<TarifaApartamento>();

    public virtual ICollection<TarifaPersonaAdicionalTemporadum> TarifaPersonaAdicionalTemporada { get; set; } = new List<TarifaPersonaAdicionalTemporadum>();
}
