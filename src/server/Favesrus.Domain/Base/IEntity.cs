using System.ComponentModel.DataAnnotations;

namespace Favesrus.Domain.Base
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
