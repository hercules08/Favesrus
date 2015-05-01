using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Models.Reccomendation
{
    public class GetReccomendationsModel
    {
        [Required]
        public string UserId { get; set; }
        public List<int> CategoryIds { get; set; }
        public int ReturnedSetNumber { get; set; }
    }
}