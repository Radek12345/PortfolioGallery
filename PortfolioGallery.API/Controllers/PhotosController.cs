using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioGallery.API.Controllers.Resources;
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

        public PhotosController(IRepository<User> repo, IPhotoService photoService)
        {
            this.photoService = photoService;
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int userId, IFormFile photo)
        {
            if (!ControllerHelper.IsCorrectUser(userId, User))
                return Unauthorized();

            var user = await repo.Get(userId);

            photoService.UploadPhoto(user, photo);


            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}