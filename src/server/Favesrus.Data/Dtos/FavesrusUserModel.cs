using System;
using System.Collections.Generic;

namespace Favesrus.Data.Dtos
{
    public class FavesrusUserModel
    {
        public FavesrusUserModel()
        {

        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ModoAccountId { get; set; }
        public DateTime? Birthday { get; set; }
        //public Gender Gender { get; set; }
        public string ProfilePic { get; set; }
        public ICollection<FaveEventModel> FaveEvents { get; set; }
        public ICollection<WishListModel> WishLists { get; set; }
        public ICollection<FavesrusUserModel> FollowFromUser { get; set; }
        public ICollection<FavesrusUserModel> FollowToUser { get; set; }
    }
}