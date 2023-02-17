﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Domain.EntityModels
{
    public class RoomBooking
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
