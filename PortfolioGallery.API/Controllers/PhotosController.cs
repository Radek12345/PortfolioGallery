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
        private readonly IRepository<User> repo;
        private readonly IPhotoService photoService;
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public PhotosController(IRepository<User> repo, IPhotoService photoService,
            IUnitOfWork unit, IMapper mapper)
        {
            this.mapper = mapper;
            this.unit = unit;
            this.photoService = photoService;
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int userId, IFormFile photoFile)
        {
            if (!ControllerHelper.IsAllowedUser(userId, User))
                return Unauthorized();

            var user = await repo.Get(userId);

            var photo = photoService.UploadPhotoToCloudinary(photoFile);

            user.Photos.Add(photo);

            if (await unit.CompleteAsync())
            {
                var photoToReturn = mapper.Map<PhotoResource>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id}, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await repo.Get(id);
            return Ok(mapper.Map<PhotoResource>(photo));
        }
    }
}