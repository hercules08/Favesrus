using System.ComponentModel.DataAnnotations;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class LoginFacebookModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string ProviderKey { get; set; }
        public string DeviceInfo { get; set; }
    }
}