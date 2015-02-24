using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class LoginFacebookModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string ProviderKey { get; set; }
    }
}