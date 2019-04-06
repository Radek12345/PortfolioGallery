using System.ComponentModel.DataAnnotations;

namespace PortfolioGallery.API.Controllers.Resources
{
    public class UserResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
        // [Required]
        // [StringLength(255, MinimumLength = 8, ErrorMessage = "You must specify password of minimum 8 characters")]
        // public string Password { get; set; }
    }
}