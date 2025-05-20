using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class ServicioSede
{
    public int IdServicio { get; set; }

    public int? IdSede { get; set; }

    public string? Servicio { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }
}
