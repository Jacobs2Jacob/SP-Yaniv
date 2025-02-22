using System.ComponentModel.DataAnnotations;
 
namespace Core.Entities
{
    public abstract class EntityBase
    {
        // Guid is better for security and uniqueness so I use it in the base type and overrides it as integer in fileBase
        [Key]
        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? ModifyDate { get; private set; }

        public EntityBase() 
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}
