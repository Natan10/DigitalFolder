using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Dtos.Wallet
{
    public class ReadWalletDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string WalletName { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }

    }
}
