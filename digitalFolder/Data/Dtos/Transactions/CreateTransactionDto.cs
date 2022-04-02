using DigitalFolder.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Dtos.Transactions
{
    public class CreateTransactionDto
    {
        [Required]
        public TransactionType Type { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Value { get; set; }
        
        [Required]
        public int WalletId { get; set; }
    }

}
