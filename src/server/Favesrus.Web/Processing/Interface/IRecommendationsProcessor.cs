using Favesrus.Data.Dtos;
using Favesrus.Data.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Favesrus.Server.Processing.Interface
{
    public interface IRecommendationsProcessor
    {
        ICollection<GiftItemModel> GetToT(GetRecommendationsModel model);
        Task<ICollection<GiftItemModel>> GetToTAsync(GetRecommendationsModel model);
    }
}
