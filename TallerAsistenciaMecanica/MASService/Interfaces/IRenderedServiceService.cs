using MASBusiness.DTO;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;

namespace MASBusiness.Interfaces
{
    public interface IRenderedServiceService
    {
        Task<ServiceResponse<RenderedServiceDto>> CreateRenderedServiceAsync(RenderedServiceDto renderedServiceDto);
        Task<ServiceResponse<RenderedServiceDto>> GetRenderedServiceByIdAsync(int id);
        Task<ServiceResponse<List<RenderedServiceDto>>> GetAllRenderedServicesByDateAsync(DateOnly dateProvided);
        Task<ServiceResponse<bool>> DeleteRenderedServiceAsync(int id);
    }
}