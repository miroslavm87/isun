using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Infrastructure.Models
{
    public interface IEntityWithId
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
