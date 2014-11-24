using Favit.DAL.Interfaces;
using Favit.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL
{
    public abstract class BaseService<TEntity> where TEntity : EntityBase
    {
        public IUnitOfWork uow { get; private set; }
        public IRepository repo { get; private set; }

        public BaseService(IRepository repo, IUnitOfWork uow)
        {
            uow = uow;
            repo = repo;
        }
    }
}
