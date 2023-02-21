﻿using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public ICollection<Booking>? Booking { get; set; }
    }
}
