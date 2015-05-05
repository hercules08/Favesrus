using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Models.Recommendation
{
    public class GetRecommendationsModel
    {
        [Required]
        public string UserId { get; set; }
        public List<int> RecommendationIds { get; set; }
        public int ReturnedSetNumber { get; set; }
    }
}