using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Favesrus.Data.RequestModels
{
    public class GetRecommendationsModel
    {
        [Required]
        public string UserId { get; set; }
        public List<int> RecommendationIds { get; set; }
        public int ReturnedSetNumber { get; set; }
    }
}