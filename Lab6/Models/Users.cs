using MongoDB.Bson;

namespace Lab6.Models
{
    public class Users
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required float Weight { get; set; }
    }
}
