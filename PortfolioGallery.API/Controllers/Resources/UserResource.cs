using System.ComponentModel.DataAnnotations;

namespace PortfolioGallery.API.Controllers.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}