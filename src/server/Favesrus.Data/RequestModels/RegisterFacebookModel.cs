using System.ComponentModel.DataAnnotations;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class RegisterFacebookModel:RegisterModel
    {
        [Required]
        public string ProviderKey { get; set; }
    }
}