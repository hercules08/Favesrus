using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.DAL.Core;
using Favesrus.Domain.Entity;
using System.Linq;

namespace Favesrus.Services
{
    public interface IRetailerService
    {
        Retailer AddRetailer(Retailer entity);
        Retailer UpdateRetailer(Retailer entity);
        Retailer FindRetailerById(int id);
        Retailer FindRetailerByName(string name);
        void DeleteRetailer(int id);
        IQueryable<Retailer> GetRetailers();
    }

    public class RetailerService : BaseService, IRetailerService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<Retailer> _retailerRepo = null;

        public RetailerService(
             ILogManager logManager,
            IAutoMapper mapper,
            IUnitOfWork uow,
            IRepository<Retailer> retailerRepo)
            : base(logManager, mapper)
        {
            _uow = uow;
            _retailerRepo = retailerRepo;
        }


        public Retailer AddRetailer(Retailer entity)
        {
            _retailerRepo.Add(entity);
            _uow.Commit();
            return entity;
        }

        public IQueryable<Retailer> GetRetailers()
        {
            return _retailerRepo.All;
        }


        public Retailer UpdateRetailer(Retailer entity)
        {
            _retailerRepo.Update(entity);
            _uow.Commit();
            return entity;
        }

        public Retailer FindRetailerById(int id)
        {
            return _retailerRepo.FindById(id);
        }

        public Retailer FindRetailerByName(string name)
        {
            return _retailerRepo.FindWhere(r => r.RetailerName == name);
        }

        public void DeleteRetailer(int id)
        {
            _retailerRepo.DeleteWhere(r => r.Id == id);
            _uow.Commit();
        }
    }
}
