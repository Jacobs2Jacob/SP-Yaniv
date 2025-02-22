
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.RequestModels
{
    public class AddImageRequest
    {
        public required IFormFile File { get; set; }
        
        [MaxLength(50)]
        public required string PictureName { get; set; }
        public DateTime? PictureDate { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}