using Favesrus.Core;
using Favesrus.DAL.Core;
using Favesrus.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using Favesrus.Data.Dtos;
using Favesrus.Core.TypeMapping;
using Favesrus.Core.Logging;

namespace Favesrus.Services
{
    public interface IGiftItemService
    {
        GiftItem AddGiftItem(GiftItem entity);
        GiftItem UpdateGiftItem(GiftItem entity);
        IQueryable<GiftItem> GiftItemsByCategoryId(int id);
        IQueryable<GiftItem> GiftItemsByCategoryName(string categoryName);
        IQueryable<GiftItem> AllGiftItems { get; }

        ICollection<GiftItemModel2> GetGiftItemsWithTerm(string searchText);
    }

    public class GiftItemService:BaseService,IGiftItemService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<GiftItem> _giftItemRepo = null;

        public GiftItemService(
            ILogManager logManager,
            IAutoMapper mapper,
            IUnitOfWork uow,
            IRepository<GiftItem> giftItemRepo)
            :base(logManager, mapper)
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


        public ICollection<GiftItemModel2> GetGiftItemsWithTerm(string searchText)
        {
            List<GiftItemModel2> foundGiftItems = new List<GiftItemModel2>();
            //Add dummy 
            foundGiftItems.Add(new GiftItemModel2() { Image = "dummy.jpg", Name = "dummy", Description = "dummy description" });
            foundGiftItems.Add(new GiftItemModel2() { Image = "dummy2.jpg", Name = "dummy2", Description = "dummy2 description" });
            
            var giftItems = _giftItemRepo.FindAllWhere(g => g.ItemName.ToLower().Contains(searchText)
                || g.Description.ToLower().Contains(searchText)).ToList();

            ICollection<GiftItemModel2> searchResults = Mapper.Map<ICollection<GiftItemModel2>>(giftItems);

            foundGiftItems.AddRange(searchResults);

            return foundGiftItems;
        }
    }
}
