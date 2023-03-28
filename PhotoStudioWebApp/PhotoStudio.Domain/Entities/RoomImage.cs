using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Domain.Entities
{
    public class RoomImage
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}
