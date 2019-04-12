using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Core.Services
{
    public interface IPhotoService
    {
        void UploadPhoto(User user, IFormFile photo); 
    }
}