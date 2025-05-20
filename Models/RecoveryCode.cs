using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models
{
    public class RecoveryCode
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Code { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
