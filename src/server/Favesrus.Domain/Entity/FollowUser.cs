using System;

namespace Favesrus.Domain.Entity
{
    public class FollowUser
    {
        public FollowUser()
        {
            AddedDate = DateTime.Now;
        }
        public int FollowUserId { get; set; }

        public string ToUserId { get; set; }

        public string FromUserId { get; set; }

        public bool Accepted { get; set; }

        public DateTime AddedDate { get; set; }

        public virtual FavesrusUser ToUser { get; set; }

        public virtual FavesrusUser FromUser { get; set; }
    }
}
