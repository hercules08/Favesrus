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
    public class GiftItemService:IGiftItemService
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
            _uow.Save();
            return entity;
        }

        public GiftItem UpdateGiftItem(GiftItem entity)
        {
            _giftItemRepo.Update(entity);
            _uow.Save();
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
