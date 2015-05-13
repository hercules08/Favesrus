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
    public class RecommendationService:IRecommendationService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<Recommendation> _recommendationRepo = null;

        public RecommendationService(
            IUnitOfWork uow,
            IRepository<Recommendation> recommendationRepo)
        {
            _uow = uow;
            _recommendationRepo = recommendationRepo;
        }

        public ICollection<GiftItem> GetReccomendationsForCategories(
            ICollection<int> categoryIds, 
            string userId, 
            int numOfReccomendations)
        {
            throw new NotImplementedException();
        }

        public ICollection<Recommendation> GetAllRecommendations()
        {
            return _recommendationRepo.All.ToList();
        }

    }
}
