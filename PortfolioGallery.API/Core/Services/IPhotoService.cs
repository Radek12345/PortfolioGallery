using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Core.Services
{
    public interface IPhotoService
    {
        Photo UploadPhotoToCloudinary(IFormFile photo); 
    }
}