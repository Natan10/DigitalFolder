using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DigitalFolder.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public virtual List<Wallet> Wallets { get; set; }
    }
}
