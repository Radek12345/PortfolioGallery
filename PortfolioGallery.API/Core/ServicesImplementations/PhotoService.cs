using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Services;

namespace PortfolioGallery.API.Core.ServicesImplementations
{
    public class PhotoService : IPhotoService
    {
        public void UploadPhoto(User user, IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
        }
    }
}