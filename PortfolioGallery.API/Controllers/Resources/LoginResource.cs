using System.ComponentModel.DataAnnotations;

namespace PortfolioGallery.API.Controllers.Resources
{
    public class LoginResource
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}