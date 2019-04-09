using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioGallery.API.Controllers.Resources;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Repositories;
using PortfolioGallery.API.Core.Services;

namespace PortfolioGallery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;
        private readonly IRepository<User> repo;
        private readonly IMapper mapper;

        public AuthController(IAuthService service, IRepository<User> repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserResource resource)
        {
            if (await repo.FirstOrDefault(u =>
                u.Email == resource.Email || u.Name == resource.Name) != null)
            {
                return BadRequest("User with that email or name already exists");
            }

            var user = mapper.Map<User>(resource);
            var registeredUser = await service.Register(user, resource.Password);

            var userToReturn = mapper.Map<UserResource>(registeredUser);
            return CreatedAtRoute("GetUser", new { controller = "Users", id = registeredUser.Id },
                userToReturn);
        }
    }

}