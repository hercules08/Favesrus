using Favit.BLL.Interfaces;
using Favit.DAL.EntityFramwork;
using Favit.DAL.Interfaces;
using Favit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Services
{    
    public class RetailerService:IRetailerService
    {
        IRepository repo;
        IUnitOfWork uow;

        public RetailerService(ISessionFactory sessionFactory)
        {
            uow = sessionFactory.CurrentUoW;
            repo = new Repository(uow);
        }

        public IQueryable<Retailer> GetRetailers()
        {
            return repo.GetList<Retailer>();
        }

        public Retailer FindRetailerById(int? id)
        {
            return repo.GetEntity<Retailer>(id);
        }

        public Retailer AddRetailer(Retailer retailer)
        {
            uow.BeginTransaction();
            repo.AddEntity(retailer);
            uow.CommitTransaction();
            
            return retailer;
        }

        public Retailer UpdateRetailer(Retailer retailer)
        {
            uow.BeginTransaction();
            repo.UpdateEntity(retailer);
            uow.CommitTransaction();

            return retailer;
        }

        public void DeleteRetailer(Retailer retailer)
        {
            uow.BeginTransaction();
            repo.DeleteEntity(retailer);
            uow.CommitTransaction();
        }
    }
}
