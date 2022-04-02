using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Dtos.Wallet
{
    public class UpdateWalletDto
    {
        public string WalletName { get; set; }

        public string Description { get; set; }

    }
}
