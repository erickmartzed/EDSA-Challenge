using MASBusiness.DTO;
using MASBusiness.Interfaces;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalAssistanceShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenderedServiceController : ControllerBase
    {
        private readonly IRenderedServiceService _renderedServiceService;

        public RenderedServiceController(IRenderedServiceService renderedServiceService)
        {
            _renderedServiceService = renderedServiceService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<RenderedServiceDto>>> CreateRenderedService(RenderedServiceDto renderedServiceDto)
        {
            ServiceResponse<RenderedServiceDto> response = await _renderedServiceService.CreateRenderedServiceAsync(renderedServiceDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{dateProvided}")]
        public async Task<ActionResult<ServiceResponse<RenderedServiceDto>>> GetAllRenderedServices(DateOnly dateProvided)
        {
            ServiceResponse<List<RenderedServiceDto>> response = await _renderedServiceService.GetAllRenderedServicesByDateAsync(dateProvided);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<RenderedServiceDto>>> DeleteRenderedService(int id)
        {
            ServiceResponse<bool> response = await _renderedServiceService.DeleteRenderedServiceAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
