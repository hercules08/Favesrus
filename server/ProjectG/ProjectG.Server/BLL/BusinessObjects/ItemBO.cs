using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectG.Server.BLL.BusinessObjects
{
    public class ItemBO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPicture { get; set; }
        public RetailerBO Retailer { get; set; }
        public CategoryBO Category { get; set; }

    }
}