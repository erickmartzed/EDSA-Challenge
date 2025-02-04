using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MASBusiness.Services
{
    public class BrandService : IBrandService
    {
        private readonly DbMechanicalAssistanceShopContext _context;

        public BrandService(DbMechanicalAssistanceShopContext context)
        {
            _context = context;
        }

        public async Task<Brand> GetOrCreateBrandAsync(string brandName)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brandName);
            if (brand == null)
            {
                brand = new Brand { Name = brandName, Valid = true };
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
            }
            return brand;
        }
    }

}
