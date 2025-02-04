using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MASBusiness.Services
{
    public class ModelService : IModelService
    {
        private readonly DbMechanicalAssistanceShopContext _context;
        private readonly IBrandService _brandService;

        public ModelService(DbMechanicalAssistanceShopContext context, IBrandService brandService)
        {
            _context = context;
            _brandService = brandService;
        }

        public async Task<Model> GetOrCreateModelAsync(string modelName, string brandName)
        {
            var model = await _context.Models
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.Name == modelName && m.Brand.Name == brandName);

            if (model == null)
            {
                var brand = await _brandService.GetOrCreateBrandAsync(brandName);
                model = new Model { Name = modelName, BrandId = brand.Id, Valid = true };
                _context.Models.Add(model);
                await _context.SaveChangesAsync();
            }
            return model;
        }
    }
}
