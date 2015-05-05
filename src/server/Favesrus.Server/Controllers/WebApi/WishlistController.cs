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
using Favesrus.Model.Entity;

namespace Favesrus.Server.Controllers.WebApi
{
    //[Authorize]
    [RoutePrefix("api/wishlist")]
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

                    if(foundWishList != null)
                    {
                        var itemOnList = foundWishList.GiftItems.Where(g => g.Id == foundItem.Id).FirstOrDefault();
                        if(itemOnList == null)
                        {
                            foundWishList.GiftItems.Add(foundItem);
                            db.SaveChanges();
                            return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                        }
                        else
                        {
                            return new BaseActionResult<GiftItem>(requestMessage,
                                foundItem,
                                "The selected item is already on this wishlist",
                                "item_is_already_on_wishlist");
                        }

                    }
                    throw new BusinessRuleException("wishlist_not_found", "The wish list could not be found");
                }
                throw new BusinessRuleException("giftitem_not_found", "The gift item could not be found");
            }
            throw new BusinessRuleException("user_not_found", "The user could not be found.");
        }

        //RemoveItemFromWishList
        [HttpPost]
        [Route("removegiftitemfromwishlist")]
        //[Authorize]
        [ValidateModel]
        public IHttpActionResult RemoveGiftItemFromWishlist(HttpRequestMessage requestMessage, WishListAddModel model)
        {
            var user = UserManager.FindById(model.UserId);

            if (user != null)
            {
                var foundItem = db.GiftItems.Find(model.GiftItemId);

                if (foundItem != null)
                {
                    var foundWishList = db.WishLists.Find(model.WishListId);

                    if (foundWishList != null)
                    {
                        var itemOnList = foundWishList.GiftItems.Where(g => g.Id == foundItem.Id).FirstOrDefault();
                        if (itemOnList == null)
                        {
                            
                            return new BaseActionResult<GiftItem>(requestMessage,
                                foundItem,
                                "The selected item is not on this wishlist",
                                "item_not_on_wishlist");
                        }
                        else
                        {
                            foundWishList.GiftItems.Remove(foundItem);
                            db.SaveChanges();
                            return new BaseActionResult<string>(requestMessage, "Successful delete from wishlist", "Successful add to wishlist", "successful_wishlist_delete");
                        }
                    }
                    throw new BusinessRuleException("wishlist_not_found", "The wish list could not be found");
                }
                throw new BusinessRuleException("giftitem_not_found", "The gift item could not be found");
            }
            throw new BusinessRuleException("user_not_found", "The user could not be found.");
        }
    }
}
