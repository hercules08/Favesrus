using Favit.Model.Entities;
using Favit.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Interfaces
{
    public interface IItemService
    {
        IQueryable<Item> GetItems();
        Item FindItemById(int? id);
        Item AddItem(Item item);
        Item UpdateItem(Item item);
        void DeleteItem(Item item);
        DumbItem GetRandomItemA();
        DumbItem GetRandomItemB(int itemAId);
    }
}
