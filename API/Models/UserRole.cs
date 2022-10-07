using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserRole
    {
        [Key]
        public int ID { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}
