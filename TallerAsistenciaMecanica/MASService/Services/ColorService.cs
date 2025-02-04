using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MASBusiness.Services
{
    public class ColorService : IColorService
    {
        private readonly DbMechanicalAssistanceShopContext _context;

        public ColorService(DbMechanicalAssistanceShopContext context)
        {
            _context = context;
        }

        public async Task<Color> GetOrCreateColorAsync(string colorName)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.Name == colorName);
            if (color == null)
            {
                color = new Color { Name = colorName, Valid = true };
                _context.Colors.Add(color);
                await _context.SaveChangesAsync();
            }
            return color;
        }
    }

}
