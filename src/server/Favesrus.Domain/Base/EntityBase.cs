namespace Favesrus.Domain.Base
{
    /// <summary>
    /// Base class for all Database Entities.
    /// Provides primary key of type int.
    /// </summary>
    public class EntityBase:IEntity
    {
        public virtual int Id { get; set; }
    }
}
