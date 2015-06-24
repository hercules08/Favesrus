using Favesrus.Domain.Entity.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Favesrus.Domain.Entity
{
    public class FavesrusUser : IdentityUser
    {
        public FavesrusUser()
        {
            //Birthday = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ModoAccountId { get; set; }
        public DateTime? Birthday { get; set; }
        public virtual Gender Gender { get; set; }
        public string ProfilePic { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<FollowUser> FollowFromUser { get; set; }
        public virtual ICollection<FollowUser> FollowToUser { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<FavesrusUser> manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
