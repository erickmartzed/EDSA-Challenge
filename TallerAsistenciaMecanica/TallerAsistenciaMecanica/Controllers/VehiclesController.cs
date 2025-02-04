using Azure;
using MASBusiness.DTO;
using MASBusiness.Interfaces;
using MechanicalAssistanceShop.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

namespace MechanicalAssistanceShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<VehicleDto>>> CreateVehicle([FromBody] VehicleDto vehicleDto)
        {
            ServiceResponse<VehicleDto> response = await _vehicleService.CreateVehicleAsync(vehicleDto);

            if (!response.Success)
            {
                return BadRequest(new ServiceResponse<VehicleDto>(false, response.Message));
            }
            return Ok(response);
        }

        [HttpGet("GetByLicensePlate/{licensePlate}")]
        public async Task<ActionResult<ServiceResponse<VehicleDto>>> GetVehicle(string licensePlate)
        {
            ServiceResponse<VehicleDto> response = await _vehicleService.GetVehicleByLicensePlateAsync(licensePlate);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<VehicleDto>>> GetVehicle(int id)
        {
            ServiceResponse<VehicleDto> response = await _vehicleService.GetVehicleByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<VehicleDto>>> UpdateVehicle([FromBody] VehicleDto vehicleDto, int id)
        {
            ServiceResponse<VehicleDto> response = await _vehicleService.UpdateVehicleAsync(id, vehicleDto);

            if (!response.Success)
            { 
                return BadRequest(response);
            }
            return Ok(new ServiceResponse<VehicleDto>(true, "Vehicle updated successfully", response.Data));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<VehicleDto>>> DeleteVehicle(int id)
        {
            ServiceResponse<VehicleDto> response = await _vehicleService.DeleteVehicleAsync(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }


}
