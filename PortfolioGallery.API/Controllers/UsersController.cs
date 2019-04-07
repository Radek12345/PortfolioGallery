using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioGallery.API.Controllers.Resources;
using PortfolioGallery.API.Core;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Repositories;

namespace PortfolioGallery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> repo;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unit;

        public UsersController(IRepository<User> repo, IMapper mapper, IUnitOfWork unit)
        {
            this.unit = unit;
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await repo.Get(id);

            if (user == null)
                return NotFound();
            
            return Ok(mapper.Map<User, UserResource>(user));
        }

        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetUsers()
        {
            var users = await repo.GetAll();

            return mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserResource resource)
        {
            var user = mapper.Map<UserResource, User>(resource);
            user.Created = DateTime.Now;

            repo.Add(user);
            await unit.CompleteAsync();

            user = await repo.Get(user.Id);
            return Ok(mapper.Map<User, UserResource>(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserResource resource)
        {
            var user = await repo.Get(id);

            if (user == null)
                return NotFound();

            mapper.Map<UserResource, User>(resource, user);
            await unit.CompleteAsync();

            return Ok(mapper.Map<User, UserResource>(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await repo.Get(id);

            if (user == null)
                return NotFound();

            repo.Remove(user);
            await unit.CompleteAsync();

            return Ok(id);
        }
    }
}