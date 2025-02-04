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
    }
}
