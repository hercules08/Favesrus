using Favesrus.Core;
using Favesrus.DAL.Core;
using Favesrus.Domain.Entity;
using System.Linq;

namespace Favesrus.Services
{
    public interface IGiftItemService
    {
        GiftItem AddGiftItem(GiftItem entity);
        GiftItem UpdateGiftItem(GiftItem entity);
        IQueryable<GiftItem> GiftItemsByCategoryId(int id);
        IQueryable<GiftItem> GiftItemsByCategoryName(string categoryName);
        IQueryable<GiftItem> AllGiftItems { get; }
    }

    public class GiftItemService:BaseService,IGiftItemService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<GiftItem> _giftItemRepo = null;

        public GiftItemService(
            IUnitOfWork uow,
            IRepository<GiftItem> giftItemRepo)
        {
            _uow = uow;
            _giftItemRepo = giftItemRepo;
        }

        public GiftItem AddGiftItem(GiftItem entity)
        {
            _giftItemRepo.Add(entity);
            _uow.Commit();
            return entity;
        }

        public GiftItem UpdateGiftItem(GiftItem entity)
        {
            _giftItemRepo.Update(entity);
            _uow.Commit();
            return entity;
        }

        public IQueryable<GiftItem> GiftItemsByCategoryId(int id)
        {
            return _giftItemRepo.FindAllWhere(x => x.Category.Where(c => c.Id == id).Count() != 0);
        }

        public IQueryable<GiftItem> GiftItemsByCategoryName(string categoryName)
        {
            return _giftItemRepo.FindAllWhere(x => x.Category.Where(c => c.CategoryName == categoryName).Count() != 0);
        }

        public IQueryable<GiftItem> AllGiftItems
        {
            get { return _giftItemRepo.All; }
        }
    }
}
