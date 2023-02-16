﻿namespace PhotoStudio.Domain.EntityModels
{
    public class EquipmentItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public string Image { get; set; }
        public Guid RoomId { get; set; }
        public Rooms Room { get; set; }
    }
}
