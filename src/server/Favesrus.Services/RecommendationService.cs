using Favesrus.Model.Entity;
using Favesrus.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    public class RecommendationService:IRecommendationService
    {
        public ICollection<GiftItem> GetReccomendationsForCategories(
            ICollection<int> categoryIds, 
            string userId, 
            int numOfReccomendations)
        {
            throw new NotImplementedException();
        }
    }
}
