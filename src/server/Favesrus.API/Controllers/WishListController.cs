using Favesrus.Core.Results;
using Favesrus.API.Filters;
using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.Data.Dtos;
using Favesrus.Data.RequestModels;
using Favesrus.Services;
using System.Threading.Tasks;
using System.Web.Http;
using Favesrus.Results;
using Favesrus.Core.Results.Error;
using System;

namespace Favesrus.API.Controllers
{
    public interface IWishListController
    {
        Task<IHttpActionResult> AddGiftItemToWishlist(WishListAddModel model);
        Task<IHttpActionResult> RemoveGiftItemFromWishlist(WishListAddModel model);
    }

    [RoutePrefix("api/wishlist")]
    public class WishListController : BaseApiController, IWishListController
    {
        IWishListService _wishListService;

        public WishListController
            (ILogManager logManager, IAutoMapper mapper,
            IWishListService wishListService)
            : base(logManager, mapper)
        {
            _wishListService = wishListService;
        }


        //AddItemToWishList
        [HttpPost]
        [Route("addgiftitemtowishlist")]
        //[Authorize]
        [ValidateModel]
        public async Task<IHttpActionResult> AddGiftItemToWishlist(WishListAddModel model)
        {
            Logger.Info("Begin");

            string apiStatus, apiMessage, errorMessage;

            var response = await _wishListService.AddGiftItemToWishListAsync(model);
            var responseAsWishListModel = response as ApiResponseModel<WishListModel2>;
            var responseAsGiftItemModel = response as ApiResponseModel<GiftItemModel>;

            if (responseAsWishListModel != null)
            {
                WishListModel2 entity = responseAsWishListModel.Model;
                apiStatus = "successful_wishlist_add";
                apiMessage = "Successful add to wishlist";

                return
                    new ApiActionResult<WishListModel2>
                        (apiStatus, apiMessage, entity);
            }
            else if (responseAsGiftItemModel != null)
            {
                GiftItemModel entity = responseAsGiftItemModel.Model;
                apiStatus = "item_is_already_on_wishlist";
                apiMessage = "The selected item is already on this wishlist";

                return
                    new ApiActionResult<GiftItemModel>
                        (apiStatus, apiMessage, entity);
            }

            Logger.Info("End");

            errorMessage = "Error occurred while adding item to wishlist";
            Logger.Error(errorMessage);
            throw new ApiErrorException(errorMessage);
        }

        //RemoveItemFromWishList
        [HttpPost]
        [Route("removegiftitemfromwishlist")]
        //[Authorize]
        [ValidateModel]
        public async Task<IHttpActionResult> RemoveGiftItemFromWishlist(WishListAddModel model)
        {
            throw new NotImplementedException();
        }
    }
}