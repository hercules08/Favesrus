using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class RegisterFacebookModel:RegisterModel
    {
        [Required]
        public string ProviderKey { get; set; }
    }
}