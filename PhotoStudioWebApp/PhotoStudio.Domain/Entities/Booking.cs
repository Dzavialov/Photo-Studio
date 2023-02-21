using Microsoft.AspNetCore.Identity;

namespace PhotoStudio.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
