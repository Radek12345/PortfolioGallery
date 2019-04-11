using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PortfolioGallery.API.Controllers.Resources
{
    public class PhotoResource
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}