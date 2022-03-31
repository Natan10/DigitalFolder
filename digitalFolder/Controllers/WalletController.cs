using DigitalFolder.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DigitalFolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        public WalletController()
        {

        }

        // TODO - Get Wallet

        [HttpPost]
        public IActionResult CreateWallet(Wallet wallet)
        {
            Console.WriteLine($"Wallet: {wallet.WalletName} / {wallet.Balance} / {wallet.Description}");
            return Ok(new {Name = wallet.WalletName , Balance = wallet.Balance , Description = wallet.Description});
        }

    }
}
