using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser
{
    public class DtoFavesrusUser
    {
        public DtoFavesrusUser()
        {
            //Birthday = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ModoAccountId { get; set; }
        public DateTime? Birthday { get; set; }
        //public Gender Gender { get; set; }
        public string ProfilePic { get; set; }
    }
}