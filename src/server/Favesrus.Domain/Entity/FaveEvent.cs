using Favesrus.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Domain.Entity
{
    public class FaveEvent:EntityBase
    {
        public string EventName { get; set; }
        public string EventImage { get; set; }
        public DateTime? EventDate { get; set; }
        public List<GiftItem> SuggestedGiftItems { get; set; }
        public FavesrusUser FavesUser { get; set; }
        public string FavesUserId { get; set; }
        public bool IsVisible { get; set; }
    }
}
