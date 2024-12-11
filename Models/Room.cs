namespace HotelSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }

        public Hotel? Hotel { get; set; }
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
}
