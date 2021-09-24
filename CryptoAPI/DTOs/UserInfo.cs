using System.ComponentModel.DataAnnotations;

namespace CryptoAPI.DTOs
{
    public class UserInfo
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
