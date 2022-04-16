using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalFolder.Models
{
    public class User
    {
        [Key, Column(name: "id")]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
