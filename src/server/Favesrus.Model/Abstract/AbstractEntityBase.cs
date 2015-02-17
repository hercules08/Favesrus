using Favesrus.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Abstract
{
    public class AbstractEntityBase:IEntity
    {
        public virtual int Id { get; set; }
    }
}
