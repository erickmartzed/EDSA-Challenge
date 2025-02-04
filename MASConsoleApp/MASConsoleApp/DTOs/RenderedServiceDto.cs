namespace MASBusiness.DTO
{
    public class RenderedServiceDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int VehicleId { get; set; }
        public int Total { get; set; }
        public DateOnly? Date { get; set; }
        public bool Valid { get; set; } = true;

        public RenderedServiceDto() { }
    }

}
