using MASBusiness.DTO;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;

namespace MASBusiness.Interfaces
{
    public interface IVehicleService
    {
        Task<ServiceResponse<VehicleDto>> CreateVehicleAsync(VehicleDto vehicleDto);
        Task<ServiceResponse<VehicleDto>> CreateVehicleAsync2(VehicleDto vehicleDto);
        Task<ServiceResponse<VehicleDto>> DeleteVehicleAsync(int id);
        Task<ServiceResponse<VehicleDto>> GetVehicleByIdAsync(int id);
        Task<ServiceResponse<VehicleDto>> GetVehicleByLicensePlateAsync(string licensePlate);
        Task<ServiceResponse<VehicleDto>> UpdateVehicleAsync(int id, VehicleDto vehicleDto);
    }
}