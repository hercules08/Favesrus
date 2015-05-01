using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.GiftItem
{
    public class DtoGiftItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
    }
}