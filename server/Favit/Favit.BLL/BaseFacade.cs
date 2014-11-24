using Favit.DAL.Interfaces;
using Favit.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL
{
    public class BaseFacade<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFacade&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public BaseFacade(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Gets or sets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The entity.</value>
        public TEntity Entity { get; private set; }
    }
}
