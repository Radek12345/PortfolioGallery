using AutoMapper;
using PortfolioGallery.API.Controllers.Resources;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResource>().ReverseMap();
            CreateMap<RegisterResource, User>();
            CreateMap<Photo, PhotoResource>().ReverseMap();
        }
    }
}