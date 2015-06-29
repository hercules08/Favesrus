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
    public interface IFaveEventController
    {
        IHttpActionResult GetUpcomingUserEvents(string userId);
        Task<IHttpActionResult> AddFaveEventForUserAsync(FaveEventModel faveEvent);
    }

    [RoutePrefix("api/faveEvent")]
    public class FaveEventController:BaseApiController, IFaveEventController
    {
        IFaveEventService _faveEventService;

        public FaveEventController(ILogManager logManager, IAutoMapper autoMapper,
            IFaveEventService faveEventService)
            :base(logManager, autoMapper)
        {
            _faveEventService = faveEventService;
        }

        public IHttpActionResult GetUpcomingUserEvents(string userId)
        {
            Logger.Info("Begin");

            string apiStatus = "get_upcoming_events";
            string apiMessage = "Found events for user " + userId;

            ICollection<FaveEventModel> foundEvents =
                _faveEventService.GetFaveEventsForUser(userId);
            
            Logger.Info("End");

            return new ApiActionResult<ICollection<FaveEventModel>>
            (apiStatus, apiMessage, foundEvents);
        }
        
        [HttpPost]
        [Route("AddFaveEventForUser")]
        public async Task<IHttpActionResult> AddFaveEventForUserAsync(FaveEventModel faveEvent)
        {
            Logger.Info("Begin");

            string apiStatus = "add_fave_event";
            string apiMessage = "Successfully added Fave Event!";

            FaveEventModel faveEventModel = await _faveEventService.AddFaveEventForUserAsync(faveEvent);
           
            Logger.Info("End");

            return new ApiActionResult<FaveEventModel>
            (apiStatus, apiMessage, faveEventModel);
        }
    }
}
