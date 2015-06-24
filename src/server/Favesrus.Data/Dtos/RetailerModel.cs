using System.Collections.Generic;

namespace Favesrus.Data.Dtos
{
    public class RetailerModel:EntityBaseModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string LogoDataString { get; set; }
        public string ModoMerchantId { get; set; }
        public virtual ICollection<GiftItemModel> Items { get; set; }
    }
}