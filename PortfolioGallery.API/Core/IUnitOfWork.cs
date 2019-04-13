using System.Threading.Tasks;

namespace PortfolioGallery.API.Core
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
    }
}