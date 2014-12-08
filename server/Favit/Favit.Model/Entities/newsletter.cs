using Favit.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Model.Entities
{
    [Table("newsletter")] 
    public class newsletter:EntityBase
    {
        public newsletter()
        {

        }

        public newsletter(string email)
        {
            this.email = email;
            this.time = DateTime.Now.ToString();
        }
        public virtual string email { get; set;}
        public virtual string time { get; set; }
    }
}
