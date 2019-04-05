using Microsoft.EntityFrameworkCore;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Persistence
{
    public class PortfolioGalleryDbContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }

        public PortfolioGalleryDbContext(DbContextOptions<PortfolioGalleryDbContext> options)
            : base(options) {}
    }
}