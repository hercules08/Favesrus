using Favit.BLL.Interfaces;
using Favit.DAL.EntityFramwork;
using Favit.DAL.Interfaces;
using Favit.Model.Entities;
using Favit.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Services
{
    public class ItemService : IItemService
    {
        IRepository repo;
        IUnitOfWork uow;

        public ItemService(ISessionFactory sessionFactory)
        {
            uow = sessionFactory.CurrentUoW;
            repo = new Repository(uow);
        }

        public IQueryable<Item> GetItems()
        {
            return repo.GetList<Item>();
        }

        public Item FindItemById(int? id)
        {
            return repo.GetEntity<Item>(id);
        }

        public Item AddItem(Item entity)
        {
            uow.BeginTransaction();
            repo.AddEntity(entity);
            uow.CommitTransaction();

            return entity;
        }

        public Item UpdateItem(Item entity)
        {
            uow.BeginTransaction();
            repo.UpdateEntity(entity);
            uow.CommitTransaction();

            return entity;
        }

        public void DeleteItem(Item entity)
        {
            uow.BeginTransaction();
            repo.DeleteEntity(entity);
            uow.CommitTransaction();
        }

        public DumbItem GetRandomItemA()
        {
            throw new NotImplementedException();
        }

        public DumbItem GetRandomItemB(int entityAId)
        {
            throw new NotImplementedException();
        }
    }
}
