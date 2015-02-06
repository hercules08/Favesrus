using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Web.Api.Models
{
    public class Item:ILinkContaining
    {
        private List<Link> _links;

        public string Id { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> RetailerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Category Category { get; set; }
        public Retailer Retailer { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}
