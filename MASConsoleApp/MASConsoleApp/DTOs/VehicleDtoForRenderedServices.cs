namespace MASBusiness.DTO
{
    public class VehicleDtoForRenderedServices : VehicleDto
    {
        public List<ServiceByRendServDto> ServiceByRendServ { get; set; } = new List<ServiceByRendServDto>();
    }

}
