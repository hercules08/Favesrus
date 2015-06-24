using System;

namespace Favesrus.Data.Dtos
{
    public class FollowUserModel : EntityBaseModel
    {
        public string ToUserId { get; set; }
        public string FromUserId { get; set; }
        public bool Accepted { get; set; }
        public DateTime AddedDate { get; set; }
        public FavesrusUserModel ToUser { get; set; }
        public FavesrusUserModel FromUser { get; set; }

    }
}