using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class ApartamentoCiudad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Apartamento> Apartamentos { get; set; } = new List<Apartamento>();
}
