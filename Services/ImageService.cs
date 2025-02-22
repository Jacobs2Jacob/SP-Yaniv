using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using DAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Services
{  
    public class ImageService : IImageService
    {
        private readonly IImageService _imageDbService;
        private readonly IConfiguration _configuration;

        // Constructor injection for the service
        public ImageService(ILogger<ImageDbService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _imageDbService = new ImageDbService(logger);
        }

        public async Task<ImageDTO[]> GetImagesAsync()
        {
            return await _imageDbService.GetImagesAsync();
        }

        /// <summary>
        /// Upload a single image to the Db
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="name"></param>
        /// <returns>The Id of the saved image</returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> SaveImageAsync(Image image)
        { 
            // Call the DAL to save the image
            return await _imageDbService.SaveImageAsync(image);
        }
    }
}
