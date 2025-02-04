using MASBusiness.DTO;
using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.EntityFrameworkCore;

namespace MASBusiness.Services
{
    public class ServiceService : IServiceService
    {
        private readonly DbMechanicalAssistanceShopContext _context;

        public ServiceService(DbMechanicalAssistanceShopContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<ServiceDto>> CreateServiceAsync(ServiceDto serviceDto)
        {
            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Title == serviceDto.Title);

            if (service == null)
            {
                service = new Service
                {
                    Title = serviceDto.Title,
                    Description = serviceDto.Description,
                    Price = serviceDto.Price,
                    Valid = true
                };
                _context.Services.Add(service);
            }
            else if (!service.Valid)
            {
                service.Valid = true;
                service.DeletedAt = null;
                service.Description = serviceDto.Description;
                service.Price = serviceDto.Price;
            }
            else
            {
                serviceDto.Id = service.Id;
                return new ServiceResponse<ServiceDto>(false, "Service already exists.", serviceDto);
            }

            await _context.SaveChangesAsync();
            serviceDto.Id = service.Id;
            return new ServiceResponse<ServiceDto>(true, "Service created successfully.", serviceDto);
        }

        public async Task<ServiceDto> GetOrCreateServiceAsync(ServiceDto serviceDto)
        {
            Service? service = await _context.Services.FirstOrDefaultAsync(s => s.Title == serviceDto.Title);

            if (service is null)
            {
                service = new Service { Title = serviceDto.Title
                                        , Description = serviceDto.Description
                                        , Price = serviceDto.Price
                                        , Valid = true
                                        , DeletedAt = null };
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
            }
            else if (service.Valid == false)
            {
                service.Valid = true;
                service.DeletedAt = null;
                await _context.SaveChangesAsync();
            }
            return serviceDto;
        }

        public async Task<ServiceDto> GetServiceAsync(ServiceDto serviceDto)
        {
            Service? service = await _context.Services.FirstOrDefaultAsync(s => s.Title == serviceDto.Title);
            if(service is not null)
            {
                serviceDto.Title = service.Title;
                serviceDto.Description = service.Description;
                serviceDto.Price = service.Price;
            }
            return serviceDto;
        }

        public async Task<ServiceResponse<ServiceDto>> UpdateServiceAsync(int id, ServiceDto serviceDto)
        {
            var service = await _context.Services
                                .Where(s => s.Id == id && s.Valid)
                                .FirstOrDefaultAsync();

            if (service == null)
            {
                return new ServiceResponse<ServiceDto>(false, "Service not found or has been deleted.", serviceDto);
            }

            service.Title = serviceDto.Title;
            service.Description = serviceDto.Description;
            service.Price = serviceDto.Price;

            await _context.SaveChangesAsync();
            return new ServiceResponse<ServiceDto>(true, "Service updated successfully.", serviceDto);
        }

        public async Task<ServiceResponse<bool>> DeleteServiceAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return new ServiceResponse<bool>(false, "Service not found.", false);
            }

            if (!service.Valid)
            {
                return new ServiceResponse<bool>(false, "Service has already been deleted.", false);
            }

            service.Valid = false;
            service.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Service deleted successfully.", true);
        }
    }
}
