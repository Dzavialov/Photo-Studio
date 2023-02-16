using Microsoft.AspNetCore.Identity;

namespace PhotoStudio.Domain.EntityModels
{
    public class Rooms
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public DateTime BookingTime { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<EquipmentItem> EquipmentItems { get; set; }
    }
}
