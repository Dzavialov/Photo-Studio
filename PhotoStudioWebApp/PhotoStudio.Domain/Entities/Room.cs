using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoStudio.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public ICollection<Booking>? Booking { get; set; }
    }
}
