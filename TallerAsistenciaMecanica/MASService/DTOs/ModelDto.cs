using MASBusiness.DTOs;
using MASDataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace MASBusiness.DTO
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public BrandDto? Brand { get; set; }
    }

}
