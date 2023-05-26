namespace PhotoStudio.Domain.Entities
{
    public class EquipmentItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageName { get; set; }
    }
}
