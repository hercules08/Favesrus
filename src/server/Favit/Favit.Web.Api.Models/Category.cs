using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Web.Api.Models
{
    public class Category:ILinkContaining
    {
        private List<Link> _links;

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Item> Items { get; set; }
       
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
