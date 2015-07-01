using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.Results.Error;
using Favesrus.Core.TypeMapping;
using Favesrus.DAL.Core;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    public interface IFaveEventService
    {
        ICollection<FaveEventModel> GetFaveEventsForUser(string userId);
        Task<FaveEventModel> AddFaveEventForUserAsync(FaveEventModel faveEventModel);
    }

    public class FaveEventService:BaseService, IFaveEventService
    {
        IUnitOfWork _uow;
        IRepository<FaveEvent> _faveEventRepo;

        public FaveEventService(
            ILogManager logManager,
            IAutoMapper mapper,
            IUnitOfWork uow,
            IRepository<FaveEvent> faveEventRepo)
            :base(logManager, mapper)
        {
            _uow = uow;
            _faveEventRepo = faveEventRepo;
        }

        public ICollection<FaveEventModel> GetFaveEventsForUser(string userId)
        {
            Logger.Info("Begin");

            ICollection<FaveEventModel> faveEventsModel = new List<FaveEventModel>();            
            var faveEvents = _faveEventRepo.FindAllWhere(f => f.FavesUserId == userId).ToList();
            Logger.Info("Found {0} events", faveEvents.Count);
            faveEventsModel = Mapper.Map<ICollection<FaveEventModel>>(faveEvents);

            Logger.Info("End");

            return faveEventsModel;
        }

        public async Task<FaveEventModel> AddFaveEventForUserAsync(FaveEventModel faveEventModel)
        {
            Logger.Info("Begin");

            FaveEvent faveEvent = Mapper.Map<FaveEvent>(faveEventModel);
            
            try
            {
                await _faveEventRepo.AddAsync(faveEvent);
            }
            catch
            {
                string errorMessage = "Unable to add Fave Event";
                throw new ApiErrorException(errorMessage,faveEventModel);
            }

            FaveEventModel resultModel = Mapper.Map<FaveEventModel>(faveEvent);

            Logger.Info("End");
            
            return resultModel;
        }
    }
}
