using DigitalFolder.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;


namespace DigitalFolder.Data.Dtos.Transactions
{
    public class ReadTransactionDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Value { get; set; }

        public int WalletId { get; set; }

        public object Wallet { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
