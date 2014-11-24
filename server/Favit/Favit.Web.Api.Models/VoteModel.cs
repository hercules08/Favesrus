using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Web.Api.Models
{
    public class VoteModel
    {
        public int XBoxCount { get; set; }
        public int PS4Count { get; set; }
        public string XBoxPrecent { get { return (((float)XBoxCount / (float)TotalCount) * 100).ToString("n2"); } }
        public string PS4Precent { get { return (((float)PS4Count / (float)TotalCount) * 100).ToString("n2"); } }
        public int TotalCount { get { return XBoxCount + PS4Count; } }
    }
}
