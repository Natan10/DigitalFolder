using DigitalFolder.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalFolder.Models
{
    public class Transaction : BaseEntity
    {
        [Key, Column(name: "id")]
        [Required]
        public int Id { get; set; }

        [Required]
        public TransactionType Type {get; set;}

        public string Description { get; set; }

        [Required]
        public decimal Value { get; set; }

        public int WalletId { get; set; }

        public virtual Wallet Wallet { get; set; }

        public string? File { get; set; }
    }
}
