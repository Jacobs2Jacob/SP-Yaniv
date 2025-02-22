
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    // the user may want to upload different media types, so using fileBase can cover this cases
    public abstract class FileBase: EntityBase
    {
        // integer id for files
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; private set; }

        [Key]
        public required string Name { get; set; }

        public required byte[] Data { get; set; }

        // For more than one user this prop is crucial
        //[ForeignKey("userId")]
        //public string UserId { get; set; }
    }
}
