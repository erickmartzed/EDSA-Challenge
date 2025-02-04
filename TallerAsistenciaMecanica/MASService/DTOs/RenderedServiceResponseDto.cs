using MASBusiness.DTOs;
using MASDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace MASBusiness.DTO
{
    public class RenderedServiceResponseDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int Total { get; set; }
        public DateOnly? Date { get; set; }
        public bool Valid { get; set; }
        public VehicleDto Vehicle { get; set; }
        public List<ServiceByRendServDto> ServiceByRendServ { get; set; } = new List<ServiceByRendServDto>();

        public RenderedServiceResponseDto(RenderedService renderedService)
        {
            Id = renderedService.Id;
            VehicleId = renderedService.VehicleId;
            Total = renderedService.Total;
            Date = renderedService.Date;
            Valid = renderedService.Valid;
            Vehicle = new VehicleDto(renderedService.Vehicle);
            ServiceByRendServ = renderedService.ServiceByRendServs
                .Where(srs => srs.Valid)
                .Select(srs => new ServiceByRendServDto(srs))
                .ToList();
        }
    }

}
