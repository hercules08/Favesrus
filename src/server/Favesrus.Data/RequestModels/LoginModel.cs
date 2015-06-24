using System.ComponentModel.DataAnnotations;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class LoginModel
    {
        /// <summary>
        /// Faves 'R' Us User Name
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Faves 'R' Us User Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}