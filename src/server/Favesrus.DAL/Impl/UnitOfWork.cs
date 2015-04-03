using Favesrus.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.DAL.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IFavesrusDbContext context;

        public UnitOfWork()
        {
            context = new FavesrusDbContext();
        }

        public UnitOfWork(IFavesrusDbContext context)
        {
            this.context = context;
        }
        public int Save()
        {
            return context.SaveChanges();
        }

        public IFavesrusDbContext Context
        {
            get
            {
                return context;
            }
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
