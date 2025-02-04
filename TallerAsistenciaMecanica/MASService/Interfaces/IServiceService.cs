using MASBusiness.DTO;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;

namespace MASBusiness.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResponse<ServiceDto>> CreateServiceAsync(ServiceDto serviceDto);
        Task<ServiceResponse<bool>> DeleteServiceAsync(int id);
        Task<ServiceDto> GetOrCreateServiceAsync(ServiceDto serviceDto);
        Task<ServiceDto> GetServiceAsync(ServiceDto serviceDto);
        Task<ServiceResponse<ServiceDto>> UpdateServiceAsync(int id, ServiceDto serviceDto);
    }
}