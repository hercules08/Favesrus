using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        IFavesrusDbContext Context { get; }
    }
}
