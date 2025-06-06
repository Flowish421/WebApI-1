using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApI_1.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        // Relation till användare
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
