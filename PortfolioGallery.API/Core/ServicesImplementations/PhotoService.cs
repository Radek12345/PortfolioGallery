using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Services;
using PortfolioGallery.API.Helpers;

namespace PortfolioGallery.API.Core.ServicesImplementations
{
    public class PhotoService : IPhotoService
    {
        private readonly IOptions<CloudinarySettings> cloudinaryOptions;
        private Cloudinary cloudinary;

        public PhotoService(IOptions<CloudinarySettings> cloudinaryOptions)
        {
            this.cloudinaryOptions = cloudinaryOptions;
            InitializeCloudinary();
        }

        private void InitializeCloudinary()
        {
            Account account = new Account(
                cloudinaryOptions.Value.CloudName,
                cloudinaryOptions.Value.ApiKey,
                cloudinaryOptions.Value.ApiSecret
            );

            cloudinary = new Cloudinary(account);
        }

        public Photo UploadPhotoToCloudinary(IFormFile photoFile)
        {
            if (!(photoFile.Length > 0))
                return null;

            Photo photo;
            
            using (var stream = photoFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(photoFile.Name, stream),
                    Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                };

                var imageUploadResult = cloudinary.Upload(uploadParams);

                photo = new Photo()
                {
                    Url = imageUploadResult.Uri.ToString(),
                    PublicId = imageUploadResult.PublicId,
                    DateAdded = DateTime.Now,
                    Name = photoFile.Name
                };
            }

            return photo;
        }

        public void DeletePhotoFromCloudinary(Photo photo)
        {
            cloudinary.Destroy(new DeletionParams(photo.PublicId));
        }
    }
}