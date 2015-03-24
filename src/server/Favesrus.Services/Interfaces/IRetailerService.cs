using Favesrus.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services.Interfaces
{
    public interface IRetailerService
    {
        Retailer AddRetailer(Retailer entity);
        Retailer UpdateRetailer(Retailer entity);
        Retailer FindRetailerById(int id);
        Retailer FindRetailerByName(string name);
        void DeleteRetailer(int id);
        IQueryable<Retailer> GetRetailers();
    }
}
