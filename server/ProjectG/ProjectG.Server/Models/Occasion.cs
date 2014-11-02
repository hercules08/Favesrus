using System;
using System.Collections.Generic;

namespace ProjectG.Server.Models
{
    public partial class Occasion
    {
        public int Id { get; set; }
        public string Occasion1 { get; set; }
        public Nullable<System.DateTime> OccasionDate { get; set; }
    }
}
