using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace DAL.Services
{  
    public class ImageDbService : IImageService
    {
        private readonly ILogger<ImageDbService> _logger;

        public ImageDbService(ILogger<ImageDbService> logger) 
        {
            _logger = logger;
        }

        public async Task<ImageDTO[]> GetImagesAsync()
        {
            ImageDTO[] res = null!;

            using (var context = new DbContextFactory().CreateDbContext())
            {
                try
                {
                    res = await context.Images
                    .Select(image => image.ToDTO())
                    .ToArrayAsync();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError($"Database error: {ex.Message}");

                    // prevent sending Db error details in message
                    throw new DatabaseException($"Internal Server Error", ex);
                }
            }

            return res;
        }

        public async Task<int> SaveImageAsync(Image image)
        {
            using (var context = new DbContextFactory().CreateDbContext())
            {
                context.Images.Add(image);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError($"Database error: {ex.Message}");

                    // duplicate key
                    if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                    {
                        throw new DatabaseException($"Key Already Exists: {image.Name}", ex);
                    }
                    // generic Db error
                    else
                    {
                        // prevent sending Db error details in message
                        throw new DatabaseException($"Internal Server Error", ex);
                    }
                }

                return image.Id;
            }
        }
    }
}