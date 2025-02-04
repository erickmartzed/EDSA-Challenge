using MASBusiness.DTOs;
using MASBusiness.Services;
using MASDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace MASBusiness.DTO
{
    public class ServiceByRendServDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int RenderedServiceId { get; set; }
        public int Price { get; set; }
        public string? Annotation { get; set; }
        public DateOnly? Date { get; set; }

        public ServiceByRendServDto() { }

        public ServiceByRendServDto(ServiceByRendServ serviceByRendServ)
        {
            Id = serviceByRendServ.Id;
            ServiceId = serviceByRendServ.ServiceId;
            RenderedServiceId = serviceByRendServ.RenderedServiceId;
            Price = serviceByRendServ.Price;
            Annotation = serviceByRendServ.Annotation;
            Date = serviceByRendServ.Date;
        }
    }
}
