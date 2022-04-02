using DigitalFolder.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public decimal Value { get; set; }

    }
}
