using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Models
{
    public class WishListAddModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GiftItemId { get; set; }
        [Required]
        public int WishListId { get; set; }
    }
}