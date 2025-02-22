
using Core.DTO;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Save a single image in Db
        /// </summary>
        /// <param name="image"></param>
        /// <returns>The id of the entity</returns>
        Task<int> SaveImageAsync(Image image);
        
        /// <summary>
        /// Get all user's images
        /// </summary>
        /// <returns>Array of user's images</returns>
        Task<ImageDTO[]> GetImagesAsync();
    }
}
