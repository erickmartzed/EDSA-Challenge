using MASBusiness.DTOs;
using MASDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace MASBusiness.DTO
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ServiceDto() { }
    }

}
