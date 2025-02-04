using MASBusiness.DTOs;
using MASDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace MASBusiness.DTO
{
    public class VehicleDtoForRenderedServices : VehicleDto
    {
        public List<ServiceByRendServDto> ServiceByRendServ { get; set; } = new List<ServiceByRendServDto>();
    }

}
