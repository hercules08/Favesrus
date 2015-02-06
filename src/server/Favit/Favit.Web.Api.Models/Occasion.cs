using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Web.Api.Models
{
    public class Occasion
    {
        public int Id { get; set; }
        public string OccasionName { get; set; }
        public Nullable<System.DateTime> OccasionDate { get; set; }
    }
}
