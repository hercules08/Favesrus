using Favesrus.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services.Interfaces
{
    public interface IGiftItemService
    {
        GiftItem AddGiftItem(GiftItem entity);
        GiftItem UpdateGiftItem(GiftItem entity);
        IQueryable<GiftItem> GiftItemsByCategoryId(int id);
        IQueryable<GiftItem> GiftItemsByCategoryName(string categoryName);
        IQueryable<GiftItem> AllGiftItems { get; }
    }
}
