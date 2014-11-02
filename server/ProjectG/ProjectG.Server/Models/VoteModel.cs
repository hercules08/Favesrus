using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectG.Server.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public int XBoxCount { get; set; }
        public int PS4Count { get; set; }

        public float XBoxPrecent { get { return XBoxCount / TotalCount; } }

        public float PS4Precent { get { return PS4Count / TotalCount; } }

        public int TotalCount { get { return XBoxCount + PS4Count; } }
    }
}