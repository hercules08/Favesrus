using Favesrus.Server.Models;
using Favesrus.DAL.Impl;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Processing;
using Favesrus.Services;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Favesrus.Server.Controllers.WebApi
{
    public class WishlistController : ApiBaseController
    {
        private FavesrusDbContext db = new FavesrusDbContext();
        private IAutoMapper _mapper;
        
        public WishlistController(IAutoMapper mapper)
        {
            _mapper = mapper;
        }

        public WishlistController
            (IAutoMapper mapper,
                FavesrusUserManager userManager,
                FavesrusRoleManager roleManager,
                IAuthenticationManager authManager)
            : base(userManager, roleManager, authManager)
        {
            _mapper = mapper;
        }


        //AddItemToWishList
        [HttpPost]
        [Route("addgiftitemtowishlist")]
        //[Authorize]
        [ValidateModel]
        public IHttpActionResult AddGiftItemToWishlist(HttpRequestMessage requestMessage, WishListAddModel model)
        {
            var user = UserManager.FindById(model.UserId);

            if (user != null)
            {
                var foundItem = db.GiftItems.Find(model.GiftItemId);

                if (foundItem != null)
                {
                    var foundWishList = db.WishLists.Find(model.WishListId);

                    //if(user.WishListItems.Where(g => g.Id == foundItem.Id).Count() == 0)
                    //{
                    //    user.WishListItems.Add(foundItem);
                    //    db.Users.Attach(user);
                    //    db.SaveChanges();

                    //    return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                    //}

                    return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                }
                throw new BusinessRuleException("giftitem_not_found", "The gift item could not be found");
            }
            throw new BusinessRuleException("user_not_found", "The user could not be found.");
        }

        //RemoveItemFromWishList
        [ValidateModel]
        public IHttpActionResult RemoveGiftItemToWishlist(HttpRequestMessage requestMessage, WishListAddModel model)
        {
            var user = UserManager.FindById(model.UserId);

            if (user != null)
            {
                var foundItem = db.GiftItems.Find(model.GiftItemId);

                if (foundItem != null)
                {
                    var foundWishList = db.WishLists.Find(model.WishListId);

                    //if(user.WishListItems.Where(g => g.Id == foundItem.Id).Count() == 0)
                    //{
                    //    user.WishListItems.Add(foundItem);
                    //    db.Users.Attach(user);
                    //    db.SaveChanges();

                    //    return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                    //}

                    return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                }
                throw new BusinessRuleException("giftitem_not_found", "The gift item could not be found");
            }
            throw new BusinessRuleException("user_not_found", "The user could not be found.");
        }
    }
}
