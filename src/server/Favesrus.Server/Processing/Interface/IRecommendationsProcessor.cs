using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Models.Recommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Server.Processing.Interface
{
    public interface IRecommendationsProcessor
    {
        ICollection<DtoGiftItem> GetToT(GetRecommendationsModel model);
        Task<ICollection<DtoGiftItem>> GetToTAsync(GetRecommendationsModel model);
    }
}
