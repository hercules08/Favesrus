using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.Data.Dtos;
using Favesrus.Results;
using Favesrus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public interface IGiftItemController
    {
        IHttpActionResult GetGiftItemsWithTerm(string searchText);
    }

    [RoutePrefix("api/giftitem")]
    public class GiftItemController: BaseApiController, IGiftItemController
    {

        IGiftItemService _giftItemService;

        public GiftItemController(ILogManager logManager, IAutoMapper mapper,
            IGiftItemService giftItemService
            ):base(logManager, mapper)
        {
            _giftItemService = giftItemService;
        }

        public IHttpActionResult GetGiftItemsWithTerm(string searchText)
        {
            Logger.Info("Begin");

            string apiStatus = "matching_products";
            string apiMessage = "Found Matches";

            ICollection<GiftItemModel2> foundMatches = 
                _giftItemService.GetGiftItemsWithTerm(searchText);

            return new
            ApiActionResult<ICollection<GiftItemModel2>>
            (apiStatus, apiMessage, foundMatches);
        }
    }
}
