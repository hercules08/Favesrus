using Favit.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.DAL.EntityFramwork
{
    public class SessionFactory : ISessionFactory
    {
        private IUnitOfWork uow;

        DbContext orm;

        public SessionFactory(FavitDBContext context)
        {
            orm = context; 
        }

        #region ISessionFactory Members

        /// <summary>
        /// Gets the current uo W.
        /// </summary>
        /// <value>The current uo W.</value>
        public IUnitOfWork CurrentUoW
        {
            get
            {
                if (uow == null)
                {
                    uow = GetUnitOfWork();
                }

                return uow;
            }
        }

        #endregion

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <returns></returns>
        private IUnitOfWork GetUnitOfWork()
        {
            //orm = new FavitDBContext();
            return new UnitOfWork(orm);
        }
    }
}
