using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Models.Reccomendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Server.Processing.Interface
{
    public interface IRecommendationsProcessor
    {
        ICollection<DtoGiftItem> GetReccomendations(GetReccomendationsModel model);
        Task<ICollection<DtoGiftItem>> GetReccomendationsAsync(GetReccomendationsModel model);
    }
}
