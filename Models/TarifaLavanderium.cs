using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class TarifaLavanderium
{
    public int IdTarifaLavanderia { get; set; }

    public decimal? Precio { get; set; }

    public string? Observaciones { get; set; }
}
