using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Data.Dtos
{
    public class FaveEventModel:EntityBaseModel
    {
        public string EventName { get; set; }
        public string EventImage { get; set; }
        public DateTime? EventDate { get; set; }
        public List<GiftItemModel2> SuggestedGiftItems { get; set; }
        public FavesrusUserModel FavesUser { get; set; }
        public string FavesUserId { get; set; }
        public bool IsVisible { get; set; }
    }
}
