using MASBusiness.DTO;
using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.EntityFrameworkCore;

namespace MASBusiness.Services
{
    public class RenderedServiceService : IRenderedServiceService
    {
        private readonly DbMechanicalAssistanceShopContext _context;

        public RenderedServiceService(DbMechanicalAssistanceShopContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<RenderedServiceDto>> CreateRenderedServiceAsync(RenderedServiceDto renderedServiceDto)
        {
            var renderedService = new RenderedService
            {
                VehicleId = renderedServiceDto.VehicleId,
                Total = renderedServiceDto.Total,
                Date = renderedServiceDto.Date,
                Valid = true
            };

            _context.RenderedServices.Add(renderedService);
            await _context.SaveChangesAsync();

            renderedServiceDto.Id = renderedService.Id;
            return new ServiceResponse<RenderedServiceDto>(true, "Rendered service created successfully.", renderedServiceDto);
        }

        public async Task<ServiceResponse<RenderedServiceDto>> GetRenderedServiceByIdAsync(int id)
        {
            var renderedService = await _context.RenderedServices
                .Where(rs => rs.Id == id && rs.Valid)
                .FirstOrDefaultAsync();

            if (renderedService == null)
            {
                return new ServiceResponse<RenderedServiceDto>(false, "Rendered service not found.", null);
            }

            return new ServiceResponse<RenderedServiceDto>(true, "Rendered service retrieved successfully.", new RenderedServiceDto(renderedService));
        }

        public async Task<ServiceResponse<List<RenderedServiceDto>>> GetAllRenderedServicesByDateAsync(DateOnly dateProvided)
        {
            var renderedServices = await _context.RenderedServices
                .Where(rs => rs.Valid && rs.Date == dateProvided)
                .Select(rs => new RenderedServiceDto(rs))
                .ToListAsync();

            return new ServiceResponse<List<RenderedServiceDto>>(true, "Rendered services retrieved successfully.", renderedServices);
        }
        public async Task<ServiceResponse<bool>> DeleteRenderedServiceAsync(int id)
        {
            var renderedService = await _context.RenderedServices.FindAsync(id);
            if (renderedService == null)
            {
                return new ServiceResponse<bool>(false, "Rendered service not found.", false);
            }

            if (!renderedService.Valid)
            {
                return new ServiceResponse<bool>(false, "Rendered service has already been deleted.", false);
            }

            renderedService.Valid = false;
            renderedService.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Rendered service deleted successfully.", true);
        }
    }

}
