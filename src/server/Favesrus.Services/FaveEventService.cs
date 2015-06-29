using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.DAL.Core;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Favesrus.Services
{
    public interface IFaveEventService
    {
        ICollection<FaveEventModel> GetFaveEventsForUser(string userId);
        FaveEventModel AddFaveEventForUser(FaveEventModel faveEventModel);
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

        public FaveEventModel AddFaveEventForUser(FaveEventModel faveEventModel)
        {
            Logger.Info("Begin");

            FaveEvent faveEvent = Mapper.Map<FaveEvent>(faveEventModel);
            _faveEventRepo.Add(faveEvent);
            _uow.Commit();

            FaveEventModel resultModel = Mapper.Map<FaveEventModel>(faveEvent);

            Logger.Info("End");
            
            return resultModel;
        }
    }
}
