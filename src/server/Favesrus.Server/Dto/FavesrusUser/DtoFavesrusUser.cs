using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class DtoFavesrusUser:ILinkContaining
    {
        public DtoFavesrusUser()
        {
            //Birthday = DateTime.Now;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ModoAccountId { get; set; }
        public DateTime? Birthday { get; set; }
        //public Gender Gender { get; set; }
        public string ProfilePic { get; set; }
        public ICollection<DtoWishlist> WishLists { get; set; }

        public List<Link> Links { get; set; }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}