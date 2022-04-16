using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Dtos.Wallet
{
    public class CreateWalletDto
    {
        
        [Required(ErrorMessage = "WalletName is required!")]
        public string WalletName { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
