using MASBusiness.DTO;
using MASBusiness.DTOs;
using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.EntityFrameworkCore;

namespace MASBusiness.Services
{
    public class ServiceByRendServService : IServiceByRendServService
    {
        private readonly DbMechanicalAssistanceShopContext _context;
        private readonly IServiceService _serviceService;

        public ServiceByRendServService(DbMechanicalAssistanceShopContext context, IServiceService serviceService)
        {
            _context = context;
            _serviceService = serviceService;
        }

        public async Task<ServiceResponse<ServiceByRendServDto>> CreateServiceByRendServ(ServiceByRendServDto serviceByRendServDto)
        {
            try
            {
                var serviceByRendServ = new ServiceByRendServ
                {
                    ServiceId = serviceByRendServDto.ServiceId,
                    RenderedServiceId = serviceByRendServDto.RenderedServiceId,
                    Price = serviceByRendServDto.Price,
                    Annotation = serviceByRendServDto.Annotation,
                    Date = serviceByRendServDto.Date,
                    Valid = true
                };

                _context.ServiceByRendServs.Add(serviceByRendServ);
                await _context.SaveChangesAsync();

                serviceByRendServDto.Id = serviceByRendServ.Id;

                return new ServiceResponse<ServiceByRendServDto>(true, "Service has been added successfully to Rendered Service.", serviceByRendServDto);
            }
            catch (Exception ex) {
                return new ServiceResponse<ServiceByRendServDto>(false, $"An unexpected error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Vehicle>> GetRenderedServicesByLicensePlateAsync(string licensePlate)
        {
            var vehicle = await _context.Vehicles
                    .Include(v => v.Color)
                    .Include(v => v.Model)
                    .Include(v => v.RenderedServices)
                    .ThenInclude(rs => rs.ServiceByRendServs)
                    .Where(v => v.Valid == true)
                    .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
            
            if (vehicle == null)
            {
                return new ServiceResponse<Vehicle>(false, "There is no vehicle associated with the provided license plate.", vehicle);
            }
            return new ServiceResponse<Vehicle>(true, "Succesfully retrieved the vehicle rendered services.", vehicle);
        }
    }
}
