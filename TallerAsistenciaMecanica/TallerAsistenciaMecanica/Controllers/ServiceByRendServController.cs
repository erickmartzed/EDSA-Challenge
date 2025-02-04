using MASBusiness.DTO;
using MASBusiness.Interfaces;
using MASDataAccess.Models;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalAssistanceShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceByRendServController : ControllerBase
    {
        private readonly IServiceByRendServService _serviceByRendServService;

        public ServiceByRendServController(IServiceByRendServService serviceByRendServService)
        {
            _serviceByRendServService = serviceByRendServService;
        }

        [HttpGet("license/{licensePlate}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> GetRenderedServicesByLicensePlate(string licensePlate)
        {
            var result = await _serviceByRendServService.GetRenderedServicesByLicensePlateAsync(licensePlate);
            if (result is null)
            {
                return NotFound("No rendered services found for the given license plate.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ServiceByRendServDto>>> InsertServiceByRendServ([FromBody] ServiceByRendServDto serviceByRendServDto)
        {
            ServiceResponse<ServiceByRendServDto> response = await _serviceByRendServService.CreateServiceByRendServ(serviceByRendServDto);
            return Ok(response);
        }
    }
}
