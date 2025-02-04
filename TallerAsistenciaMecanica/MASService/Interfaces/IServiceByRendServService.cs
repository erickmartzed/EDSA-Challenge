using MASBusiness.DTO;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;

namespace MASBusiness.Interfaces
{
    public interface IServiceByRendServService
    {
        Task<ServiceResponse<ServiceByRendServDto>> CreateServiceByRendServ(ServiceByRendServDto serviceByRendServDto);
        Task<ServiceResponse<Vehicle>> GetRenderedServicesByLicensePlateAsync(string licensePlate);
    }
}