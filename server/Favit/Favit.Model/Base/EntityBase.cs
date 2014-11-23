using Microsoft.Practices.EnterpriseLibrary.Validation;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Favit.Model.Base
{
    public abstract class EntityBase:IEntityBase,IObjectState
    {
        public virtual int Id { get; set; }
        [NotMapped]
        public virtual bool IsValid { get; private set; }
        [NotMapped]
        public ValidationResults Errors { get; private set; }
        protected bool Validate<T>()
        {
            Errors = ValidationFactory.CreateValidator<T>().Validate(this);
            return Errors.IsValid;
        }

        [NotMapped]
        public ObjectState ObjectState
        {
            get;
            set;
        }
    }
}