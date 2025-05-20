using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
