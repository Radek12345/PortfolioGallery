using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioGallery.API.Controllers.Resources;
using PortfolioGallery.API.Core;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Repositories;
using PortfolioGallery.API.Core.Services;
using PortfolioGallery.API.Helpers;

namespace PortfolioGallery.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/{userId}")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IPhotoService photoService;
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        private readonly IRepository<Photo> photoRepo;

        public PhotosController(IUserRepository userRepo, IRepository<Photo> photoRepo, IPhotoService photoService,
            IUnitOfWork unit, IMapper mapper)
        {
            this.photoRepo = photoRepo;
            this.mapper = mapper;
            this.unit = unit;
            this.photoService = photoService;
            this.userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos([FromQuery] FilterResource filter)
        {
            IEnumerable<Photo> photos;

            if (filter.PhotoName != null)
                photos = await photoRepo.Find(p => p.Name.Contains(filter.PhotoName));
            else
                photos = await photoRepo.GetAll();

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int userId, IFormFile photoFile)
        {
            if (!ControllerHelper.IsAllowedUser(userId, User))
                return Unauthorized();

            var user = await userRepo.GetUserEager(userId);

            var photo = photoService.UploadPhotoToCloudinary(photoFile);

            user.Photos.Add(photo);

            if (await unit.CompleteAsync())
            {
                var photoToReturn = mapper.Map<PhotoResource>(photo);
                return CreatedAtRoute("GetPhoto", new { photoId = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpGet("{photoId}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int photoId)
        {
            var photo = await photoRepo.Get(photoId);
            return Ok(mapper.Map<PhotoResource>(photo));
        }

        [HttpPut("{photoId}")]
        public async Task<IActionResult> UpdatePhotoInformation(int userId, int photoId, PhotoResource resource)
        {
            if (!ControllerHelper.IsAllowedUser(userId, User))
                return Unauthorized();

            var photo = await photoRepo.Get(photoId);

            if (photo == null)
                return NotFound();

            photo.Name = resource.Name;
            photo.Description = resource.Description;

            await unit.CompleteAsync();

            return Ok(mapper.Map<PhotoResource>(photo));
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(int userId, int photoId)
        {
            if (!ControllerHelper.IsAllowedUser(userId, User))
                return Unauthorized();

            var user = await userRepo.GetUserEager(userId);

            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            
            if (photo == null)
                return NotFound();

            photoRepo.Remove(photo);

            if (await unit.CompleteAsync())
            {
                photoService.DeletePhotoFromCloudinary(photo);
                return Ok(photoId);
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}