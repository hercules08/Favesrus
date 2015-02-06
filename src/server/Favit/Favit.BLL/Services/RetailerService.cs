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

        public Retailer AddRetailer(Retailer entity)
        {
            uow.BeginTransaction();
            repo.AddEntity(entity);
            uow.CommitTransaction();
            
            return entity;
        }

        public Retailer UpdateRetailer(Retailer entity)
        {
            uow.BeginTransaction();
            repo.UpdateEntity(entity);
            uow.CommitTransaction();

            return entity;
        }

        public void DeleteRetailer(Retailer entity)
        {
            uow.BeginTransaction();
            repo.DeleteEntity(entity);
            uow.CommitTransaction();
        }
    }
}
