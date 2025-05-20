using Microsoft.AspNetCore.Identity;
namespace Prueba_Tecnica.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NombreCompleto { get; set; }
    }
}
