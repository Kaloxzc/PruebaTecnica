using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models;

public class ReservaApartamento
{
    public int IdReserva { get; set; }

    public int IdApartamento { get; set; }
    public virtual Apartamento? Apartamento { get; set; }

    public int IdTemporada { get; set; }
    public virtual Temporadum? Temporada { get; set; }

    public DateTime FechaLlegada { get; set; }
    public DateTime FechaSalida { get; set; }

    public int Noches { get; set; }
    public int NumeroPersonas { get; set; }

    public bool IncluyeLavanderia { get; set; }
    public decimal PrecioTotal { get; set; }
    public byte[]? ImagenReserva { get; internal set; }
    [NotMapped]
    public string? ImagenDataURL { get; internal set; }
}

