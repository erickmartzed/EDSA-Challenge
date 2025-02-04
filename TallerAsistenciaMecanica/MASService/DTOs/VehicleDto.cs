using MASBusiness.DTOs;
using MASDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace MASBusiness.DTO
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string? LicensePlate { get; set; }
        public string? ChassisNumber { get; set; }
        public string? EngineNumber { get; set; }
        public short ManufacturingYear { get; set; }
        public ColorDto? Color { get; set; }
        public ModelDto? Model { get; set; }

        public VehicleDto() { }
        public VehicleDto(Vehicle vehicle)
        {
            Id = vehicle.Id;
            LicensePlate = vehicle.LicensePlate;
            ChassisNumber = vehicle.ChassisNumber;
            EngineNumber = vehicle.EngineNumber;
            ManufacturingYear = vehicle.ManufacturingYear;

            Color = new ColorDto();

            Color.Id = vehicle.Color.Id;
            Color.Name = vehicle.Color.Name;

            Model = new ModelDto();

            Model.Id = vehicle.Model.Id;
            Model.Name = vehicle.Model.Name;

            Model.Brand = new BrandDto()
            {
                Id = vehicle.Model.Brand.Id,
                Name = vehicle.Model.Brand.Name
            };
            
        }
    }

}
