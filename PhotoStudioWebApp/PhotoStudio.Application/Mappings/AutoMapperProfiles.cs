using AutoMapper;
using PhotoStudio.Application.DTOs;
using PhotoStudio.Domain.Entities;

namespace PhotoStudio.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookingDto, Booking>();
            CreateMap<RoomDto, Room>();
            CreateMap<EquipmentItemDto, EquipmentItem>();
        }
    }
}
