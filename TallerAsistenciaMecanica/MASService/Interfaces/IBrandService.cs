using MASDataAccess.Models;

namespace MASBusiness.Interfaces
{
    public interface IBrandService
    {
        Task<Brand> GetOrCreateBrandAsync(string brandName);
    }
}