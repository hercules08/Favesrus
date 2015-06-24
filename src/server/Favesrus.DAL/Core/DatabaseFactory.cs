using System;

namespace Favesrus.DAL.Core
{
    public interface IDatabaseFactory : IDisposable
    {
        FavesrusDbContext Get();
    }

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private FavesrusDbContext _dataContext;

        public FavesrusDbContext Get()
        {
            return _dataContext ?? (_dataContext = new FavesrusDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
