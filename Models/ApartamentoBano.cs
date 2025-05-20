using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class ApartamentoBano
{
    public int Id { get; set; }

    public int? IdApartamento { get; set; }

    public int? Cantidad { get; set; }

    public bool? TieneBanoPrivado { get; set; }

    public virtual Apartamento? IdApartamentoNavigation { get; set; }
}
