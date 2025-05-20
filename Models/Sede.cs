using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class Sede
{
    public int IdSede { get; set; }

    public string? Nombre { get; set; }

    public int? CapacidadTotal { get; set; }

    public virtual ICollection<Alojamiento> Alojamientos { get; set; } = new List<Alojamiento>();

    public virtual ICollection<ServicioSede> ServicioSedes { get; set; } = new List<ServicioSede>();
}
