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
using log4net.Repository.Hierarchy;

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

            Log.Info("Returning " + model.ReturnedSetNumber + " sets.");
            
            foreach(var recId in model.RecommendationIds)
            {
                Log.Info("Searching for items with ids: " + recId);
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
                dtoGiftItems.Add(Mapper.Map<DtoGiftItem>(giftItem));
                Log.Info("Adding gift item " + giftItem.ItemName);
            }

            return dtoGiftItems;
        }

        public Task<ICollection<DtoGiftItem>> GetToTAsync(GetRecommendationsModel model)
        {
            return System.Threading.Tasks.Task.FromResult(GetToT(model));
        }
    }
}