using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.Models
{
    public class UserModel
    {
        [Required]
        [DefaultValue("admin")]
        public string Username { get; set; }
        [Required]
        [DefaultValue("admin")]
        public string Password { get; set; }
    }
}
