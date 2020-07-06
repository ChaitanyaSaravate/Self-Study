using System;

namespace Entities
{
    public class Block : Room
    {
        public int Capacity { get; set; }

        public int Rooms { get; set; }

        public string Name { get; set; }

        public bool IsBooked { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }
    }
}
