using Microsoft.AspNetCore.Http;
using PhotoStudio.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.Domain.Entities
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public ICollection<BookingDto>? Booking { get; set; }
        public ICollection<RoomImageDto>? Images { get; set; }
    }
}
