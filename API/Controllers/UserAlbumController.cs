using Core.Entities;
using Core.Interfaces;
using Core.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc; 

namespace SP_Yaniv.Server.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    public class UserAlbumController : ControllerBase
    { 
        private readonly ILogger<UserAlbumController> _logger;
        private readonly IImageService _imageService;

        public UserAlbumController(ILogger<UserAlbumController> logger, IImageService imageService)
        {
            _imageService = imageService;
            _logger = logger;
        }
         
        /// <summary>
        /// Retrieve all pictures
        /// </summary>
        /// <returns></returns>
        [HttpGet("pictures")]
        public async Task<IActionResult> Pictures([FromQuery] string? userId)
        {
            try
            {
                var images = await _imageService.GetImagesAsync();
                return Ok(images);
            }
            // exception in Db query
            catch (DatabaseException ex)
            {
                // prevent sending Db error details to client
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Generic Error: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

        /// <summary>
        /// Adds a single picture
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("pictures")]
        public async Task<IActionResult> Pictures([FromForm] AddImageRequest request)
        {
            // validate model
            if (ModelState.IsValid)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await request.File.CopyToAsync(memoryStream);
                        var byteArr = memoryStream.ToArray();

                        int id = await _imageService.SaveImageAsync(new Image 
                        {
                            Data = byteArr,
                            Name = request.File.FileName,
                            PictureName = request.PictureName,
                            Description = request.Description,
                            PictureDate = request.PictureDate
                        });

                        return Ok(id);
                    }
                }
                // file stream error
                catch (IOException ex)
                {
                    _logger.LogError($"I/O error: {ex.Message}");
                    return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
                }
                // exception in Db insertion
                catch (DatabaseException ex)
                {
                    // prevent sending Db error details to client
                    return StatusCode(500, new { message = ex.Message });
                }
                // generic exception
                catch (Exception ex)
                {
                    _logger.LogError($"Generic Error: {ex.Message}");
                    return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
                }
            }
            // bad request
            else
            {
                return BadRequest();
            }
            
        }
    }
}