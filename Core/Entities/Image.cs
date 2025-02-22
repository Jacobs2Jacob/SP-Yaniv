
using Core.DTO;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Image : FileBase
    {
        [MaxLength(50)]
        public required string PictureName { get; set; }
        public DateTime? PictureDate { get; set; }
        
        [MaxLength(250)]
        public string? Description { get; set; }

        public ImageDTO ToDTO() 
        {
            return new ImageDTO
            {
                Id = Id,
                Name = PictureName
            };
        }
    }
}
