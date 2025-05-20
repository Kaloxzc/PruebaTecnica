using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models;

public partial class Apartamento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdCiudad { get; set; }
   
    public bool EsHabitacion { get; set; }

    public int? CapacidadMaxima { get; set; }

    public bool? TieneSalaComedor { get; set; }

    public bool? TieneCocina { get; set; }

    public bool? TieneParqueadero { get; set; }

    public int? CantidadHabitaciones { get; set; }

    public byte[]? ImagenApartamento { get; set; }

    public virtual ICollection<ApartamentoBano> ApartamentoBanos { get; set; } = new List<ApartamentoBano>();

    public virtual ICollection<ApartamentoCama> ApartamentoCamas { get; set; } = new List<ApartamentoCama>();

    public virtual ApartamentoCiudad? IdCiudadNavigation { get; set; }

    public virtual ICollection<TarifaApartamento> TarifaApartamentos { get; set; } = new List<TarifaApartamento>();
    [NotMapped] 
    public string? ImagenDataURL { get; set; }
}
