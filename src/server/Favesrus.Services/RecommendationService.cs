using Favesrus.Core;
using Favesrus.DAL.Core;
using Favesrus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Favesrus.Services
{
    public interface IRecommendationService
    {
        ICollection<GiftItem> GetReccomendationsForCategories(ICollection<int> categoryIds, string userId, int numOfReccomendations);
        ICollection<Recommendation> GetAllRecommendations();
    }

    public class RecommendationService:BaseService,IRecommendationService
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
