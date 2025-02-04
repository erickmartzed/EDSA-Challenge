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
    }

}
