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
    }

}
