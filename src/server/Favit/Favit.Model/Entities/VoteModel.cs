using Favit.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favit.Model.Entities
{
    public class VoteModel : EntityBase
    {
        public virtual int XBoxCount { get; set; }
        public virtual int PS4Count { get; set; }
        public virtual string XBoxPrecent { get { return (((float)XBoxCount / (float)TotalCount)*100).ToString("n2"); } }
        public virtual string PS4Precent { get { return (((float)PS4Count / (float)TotalCount)*100).ToString("n2"); } }
        public virtual int TotalCount { get { return XBoxCount + PS4Count; } }
    }
}