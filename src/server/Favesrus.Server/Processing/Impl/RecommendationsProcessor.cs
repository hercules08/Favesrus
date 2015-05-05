using System.Linq;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models.Recommendation;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services;
using Favesrus.Services.Interfaces;
using Microsoft.Owin.Security;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Favesrus.Server.Processing.Impl
{
    public class RecommendationsProcessor : BaseProcessor, IRecommendationsProcessor
    {
        IRecommendationService _recommendationService;
        IGiftItemService _giftItemService;

        public RecommendationsProcessor(IEmailer emailer, 
            IAutoMapper mapper, 
            IRecommendationService recommendationSerivce, 
            IGiftItemService giftItemService)
            : base(emailer, mapper)
        {
            _recommendationService = recommendationSerivce;
            _giftItemService = giftItemService;
        }

        public RecommendationsProcessor(
                  FavesrusUserManager userManager,
                  FavesrusRoleManager roleManager,
                  IAuthenticationManager authManager,
                  IEmailer emailer,
                  IAutoMapper mapper)
            : base(userManager, roleManager, authManager, emailer, mapper)
        {
        }

        public ICollection<DtoGiftItem> GetToT(GetRecommendationsModel model)
        {
            List<DtoGiftItem> dtoGiftItems = new List<DtoGiftItem>();

            // Find user
            var user = UserManager.FindById(model.UserId);

            // Get users wishlist
            var wishlist = user.WishLists;
            
            // Get union of users gift items from all wishlist
            IEnumerable<GiftItem> giftItems = wishlist.SelectMany(w => w.GiftItems);

            // Get list of giftitems that are not currently on the users
            // wishlist and are in the reccomendations categories
            // passed in through the model

            var giftItemsMatchingRecommendationIds = from gi in _giftItemService.AllGiftItems
                                                     join id in model.RecommendationIds on gi.Id equals id
                                                     select gi;



            return dtoGiftItems;
        }

        public Task<ICollection<DtoGiftItem>> GetToTAsync(GetRecommendationsModel model)
        {
            return System.Threading.Tasks.Task.FromResult(GetToT(model));
        }
    }
}