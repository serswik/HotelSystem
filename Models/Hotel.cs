﻿namespace HotelSystem.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }

        public ICollection<Room>? Rooms { get; set; }
    }
}
