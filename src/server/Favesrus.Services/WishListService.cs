using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.Results;
using Favesrus.Core.Results.Error;
using Favesrus.Core.TypeMapping;
using Favesrus.DAL.Core;
using Favesrus.Data.Dtos;
using Favesrus.Data.RequestModels;
using Favesrus.Domain.Entity;
using Favesrus.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    public interface IWishListService
    {
        Task<object> AddGiftItemToWishListAsync(WishListAddModel model);
        Task<ApiResponseModel<GiftItemModel>> RemoveGiftItemFromWishlist(WishListAddModel model);
    }

    public class WishListService: BaseService, IWishListService
    {
        IRepository<GiftItem> _giftItemRepo;
        IRepository<WishList> _wishListRepo;
        IUnitOfWork _uow;
        FavesrusUserManager _userManager;

        public WishListService(
            ILogManager logManager,
            IAutoMapper mapper,
            IRepository<WishList> wishListRepo,
            IRepository<GiftItem> giftItemRepo,
            IUnitOfWork uow,
            FavesrusUserManager userManager)
            :base(logManager, mapper)
        {
            _wishListRepo = wishListRepo;
            _userManager = userManager;
            _giftItemRepo = giftItemRepo;
            _uow = uow;
        }
        
        public async Task<object> AddGiftItemToWishListAsync(WishListAddModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user != null)
            {
                var foundItem = _giftItemRepo.FindById(model.GiftItemId);

                if (foundItem != null)
                {
                    var foundWishList =_wishListRepo.FindById(model.WishListId);

                    if (foundWishList != null)
                    {
                        var itemOnList = foundWishList.GiftItems.Where(g => g.Id == foundItem.Id).FirstOrDefault();
                        if (itemOnList == null)
                        {
                            foundWishList.GiftItems.Add(foundItem);
                            _wishListRepo.Update(foundWishList);
                            _uow.Commit();
                            //await _wishListRepo.UpdateAsync(foundWishList);
                            WishListModel2 wishListModel = Mapper.Map<WishListModel2>(foundWishList);

                            return new ApiResponseModel<WishListModel2>(true, wishListModel);
                            
                            //ApiActionResult<>
                            //(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                        }
                        else
                        {

                            GiftItemModel foundItemModel = Mapper.Map<GiftItemModel>(foundItem);
                            return new ApiResponseModel<GiftItemModel>(true, foundItemModel);

                            //return new BaseActionResult<GiftItem>(requestMessage,
                            //    foundItem,
                            //    "The selected item is already on this wishlist",
                            //    "item_is_already_on_wishlist");
                        }

                    }
                    throw new ApiErrorException("The wish list could not be found","wishlist_not_found");
                }
                throw new ApiErrorException("The gift item could not be found", "giftitem_not_found");
            }
            throw new ApiErrorException("The user could not be found.", "user_not_found");
        }

        public Task<ApiResponseModel<GiftItemModel>> RemoveGiftItemFromWishlist(WishListAddModel model)
        {
            throw new NotImplementedException();
        }
    }
}
