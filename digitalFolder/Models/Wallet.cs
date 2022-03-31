using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalFolder.Models
{
    public class Wallet
    {
        [Required]
        [Key, Column(name: "id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "WalletName is required!")]
        [StringLength(300, MinimumLength = 5)]
        public string WalletName { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }

    }
}
