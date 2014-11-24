using Favit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Interfaces
{
    public interface IRetailerService
    {
        IQueryable<Retailer> GetRetailers();

        Retailer FindRetailerById(int? id);

        Retailer AddRetailer(Retailer retailer);

        Retailer UpdateRetailer(Retailer retailer);

        void DeleteRetailer(Retailer retailer);
    }
}
