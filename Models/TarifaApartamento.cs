using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models;

public partial class TarifaApartamento
{
    public int IdTarifa { get; set; }

    public int? Id { get; set; }

    public int? IdTemporada { get; set; }

    public int? Precio { get; set; }

    public byte[]? ImagenAlojamiento { get; set; }

    public int? PersonasIncluidas { get; set; }

    public virtual Apartamento? IdNavigation { get; set; }

    public virtual Temporadum? IdTemporadaNavigation { get; set; }
    [NotMapped]
    public string? ImagenDataURL { get; set; }
}
