using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Dtos.Wallet
{
    public class ReadWalletDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string WalletName { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public object Transactions { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
