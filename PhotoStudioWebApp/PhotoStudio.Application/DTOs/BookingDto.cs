namespace PhotoStudio.Application.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
        public Guid RoomId { get; set; }
        public string UserId { get; set; }
    }
}
