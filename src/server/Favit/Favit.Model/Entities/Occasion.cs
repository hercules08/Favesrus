using Favit.Model.Base;
using System;
using System.Collections.Generic;

namespace Favit.Model.Entities
{
    public partial class Occasion : EntityBase
    {
        public virtual string OccasionName { get; set; }
        public Nullable<System.DateTime> OccasionDate { get; set; }
    }
}
