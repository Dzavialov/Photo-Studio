using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.Domain.EntityModels
{
    public class Room
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public ICollection<EquipmentItem> EquipmentItems { get; set; }
        public ICollection<RoomBooking> RoomBooking { get; set; }
    }
}
