using Favit.BLL.Interfaces;
using Favit.Model.Entities;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Services
{    
    public class RetailerService:Service<Retailer>, IRetailerService
    {

        IUnitOfWorkAsync uow;

        public RetailerService(IRepositoryAsync<Retailer> repo, IUnitOfWorkAsync uow) : base(repo)
        {
            this.uow = uow;
        }

        public override void Insert(Retailer entity)
        {
            base.Insert(entity);

            try
            {
                uow.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
