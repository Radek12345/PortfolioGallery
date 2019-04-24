using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PortfolioGallery.API.Controllers.Resources
{
    public class PhotoResource
    {   
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int UserId { get; set; }
    }
}