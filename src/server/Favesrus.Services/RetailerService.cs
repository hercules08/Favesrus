using Favesrus.DAL.Abstract;
using Favesrus.Model.Entity;
using Favesrus.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    public class RetailerService : IRetailerService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<Retailer> _retailerRepo = null;

        public RetailerService(
            IUnitOfWork uow,
            IRepository<Retailer> retailerRepo)
        {
            _uow = uow;
            _retailerRepo = retailerRepo;
        }


        public Retailer AddRetailer(Retailer entity)
        {
            _retailerRepo.Add(entity);
            _uow.Save();
            return entity;
        }

        public IQueryable<Retailer> GetRetailers()
        {
            return _retailerRepo.All;
        }


        public Retailer UpdateRetailer(Retailer entity)
        {
            _retailerRepo.Update(entity);
            _uow.Save();
            return entity;
        }

        public Retailer FindRetailerById(int id)
        {
            return _retailerRepo.Get(id);
        }

        public Retailer FindRetailerByName(string name)
        {
            return _retailerRepo.FindBy(r => r.RetailerName == name);
        }

        public void DeleteRetailer(int id)
        {
            _retailerRepo.Delete(id);
            _uow.Save();
        }
    }
}
