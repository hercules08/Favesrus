using System.ComponentModel.DataAnnotations;

namespace Favesrus.Data.RequestModels
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