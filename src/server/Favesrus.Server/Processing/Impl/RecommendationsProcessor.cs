using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models.Reccomendation;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services;
using Favesrus.Services.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Favesrus.Server.Processing.Impl
{
    public class RecommendationsProcessor : BaseProcessor, IRecommendationsProcessor
    {
        IRecommendationService _recommendationService;

        public RecommendationsProcessor(IEmailer emailer, IAutoMapper mapper)
            : base(emailer, mapper)
        {

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

        public ICollection<DtoGiftItem> GetReccomendations(GetReccomendationsModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DtoGiftItem>> GetReccomendationsAsync(GetReccomendationsModel model)
        {
            return System.Threading.Tasks.Task.FromResult(GetReccomendations(model));
        }
    }
}