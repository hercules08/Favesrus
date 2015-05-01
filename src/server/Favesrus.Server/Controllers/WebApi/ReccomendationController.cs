using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models.Reccomendation;
using Favesrus.Server.Processing;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Controllers.WebApi
{
    public class ReccomendationController : ApiBaseController
    {
        IAutoMapper _mapper;
        IRecommendationsProcessor _recommendationsProcessor;

        public ReccomendationController(IAutoMapper mapper, 
            IRecommendationsProcessor recommendationProcessor)
        {
            _mapper = mapper;
            _recommendationsProcessor = recommendationProcessor;
        }

        public ReccomendationController()
        {
            
        }

        [ValidateModel]
        public async Task<IHttpActionResult> GetReccomendations(
            HttpRequestMessage requestMessage, 
            GetReccomendationsModel model)
        {
            string successStatus = "get_reccomendations_success";
            string successMessage = "Successfully retireved recommendations";

            ICollection<DtoGiftItem> giftItems = await _recommendationsProcessor.GetReccomendationsAsync(model);

            return new BaseActionResult<ICollection<DtoGiftItem>>(
                requestMessage,
                giftItems,
                successMessage,
                successStatus);
        }

    }
}