using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.Data.Dtos;
using Favesrus.Data.RequestModels;
using Favesrus.Domain.Entity;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Favesrus.Server.Processing.Impl
{
    public class RecommendationsProcessor : BaseService, IRecommendationsProcessor
    {
        IRecommendationService _recommendationService;
        IGiftItemService _giftItemService;

        public RecommendationsProcessor( 
            ILogManager logManager,
            IAutoMapper mapper,
            IEmailService emailer,
            IRecommendationService recommendationSerivce, 
            IGiftItemService giftItemService)
            :base(logManager, mapper)
        {
            _recommendationService = recommendationSerivce;
            _giftItemService = giftItemService;
        }

        public RecommendationsProcessor(
            ILogManager logManager,
            IAutoMapper mapper,
                  FavesrusUserManager userManager,
                  FavesrusRoleManager roleManager,
                  IAuthenticationManager authManager,
                  IEmailService emailer)
            :base(logManager, mapper)
        {
        }

        public ICollection<GiftItemModel> GetToT(GetRecommendationsModel model)
        {
            List<GiftItemModel> dtoGiftItems = new List<GiftItemModel>();

            Logger.Info("Returning " + model.ReturnedSetNumber + " sets.");
            
            foreach(var recId in model.RecommendationIds)
            {
                Logger.Info("Searching for items with ids: " + recId);
            }

            var rItems = _recommendationService.GetAllRecommendations();

            List<Recommendation> matchingRecs = new List<Recommendation>();
            
            foreach(var recId in model.RecommendationIds)
            {
                //find matching rec
                Recommendation  matchingRec = rItems.FirstOrDefault(r => r.Id == recId);

                if(matchingRec != null)
                    matchingRecs.Add(matchingRec);
            }

            //// Find user
            //var user = UserManager.FindById(model.UserId);

            //// Get users wishlist
            //var wishlist = user.WishLists;
            
            //// Get union of users gift items from all wishlist
            //IEnumerable<GiftItem> giftItems = wishlist.SelectMany(w => w.GiftItems);

            // Get list of giftitems that are not currently on the users
            // wishlist and are in the reccomendations categories
            // passed in through the model

            List<GiftItem> giftItemsMatchingRecommendationIds  = new List<GiftItem>();



                foreach(var giftItem in _giftItemService.AllGiftItems)
                {
                    foreach(var category in giftItem.Category)
                    {
                        foreach(var rec in matchingRecs)
                        {
                            if(category.Id == rec.Id)
                            {
                                giftItemsMatchingRecommendationIds.Add(giftItem);
                            }
                        }
                    }
                }

                var foundItems = giftItemsMatchingRecommendationIds.Take(model.ReturnedSetNumber * 2);
                
                //(from gi in _giftItemService.AllGiftItems
                //                                     where gi c
                //                                      join id in model.RecommendationIds on gi.Id equals id
                //                                     select gi).Take(model.ReturnedSetNumber *2);

            foreach (var giftItem in giftItemsMatchingRecommendationIds)
            {
                dtoGiftItems.Add(Mapper.Map<GiftItemModel>(giftItem));
                Logger.Info("Adding gift item " + giftItem.ItemName);
            }

            return dtoGiftItems;
        }

        public Task<ICollection<GiftItemModel>> GetToTAsync(GetRecommendationsModel model)
        {
            return System.Threading.Tasks.Task.FromResult(GetToT(model));
        }
    }
}