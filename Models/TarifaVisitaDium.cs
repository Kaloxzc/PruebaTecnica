using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class TarifaVisitaDium
{
    public int IdTarifaVisitaDia { get; set; }

    public decimal? Precio { get; set; }

    public int? MinimoPersonas { get; set; }

    public int? MaximoPersonas { get; set; }

    public string? Observaciones { get; set; }
}
