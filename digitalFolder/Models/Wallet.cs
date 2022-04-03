using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalFolder.Models
{
    public class Wallet : BaseEntity
    {
        [Key, Column(name: "id")]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "WalletName is required!")]
        public string WalletName { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

    }
}
