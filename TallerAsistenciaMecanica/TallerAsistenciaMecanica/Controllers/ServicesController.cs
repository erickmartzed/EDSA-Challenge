using MASBusiness.DTO;
using MASBusiness.Interfaces;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalAssistanceShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _services;
        public ServicesController(IServiceService services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ServiceDto>>> InsertService([FromBody] ServiceDto serviceDto)
        {
            ServiceResponse<ServiceDto> response = await _services.CreateServiceAsync(serviceDto);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<ServiceDto>>> UpdateService(int id, [FromBody] ServiceDto serviceDto)
        {
            ServiceResponse<ServiceDto> response = await _services.UpdateServiceAsync(id, serviceDto);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteService(int id)
        {
            ServiceResponse<bool> response = await _services.DeleteServiceAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
