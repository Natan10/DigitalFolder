using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Requests
{
    public class SendResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
