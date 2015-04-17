using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class LoginModel
    {
        /// <summary>
        /// Faves 'R' Us User Name
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Faves 'R' Us User Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}