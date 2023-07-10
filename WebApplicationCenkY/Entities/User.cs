using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationCenkY.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required,StringLength(50)]
        public string UserName { get; set; }
        [Required,StringLength(100)]
        public string Password { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
