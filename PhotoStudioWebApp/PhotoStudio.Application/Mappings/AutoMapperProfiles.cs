using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
