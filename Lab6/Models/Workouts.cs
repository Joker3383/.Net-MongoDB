using MongoDB.Bson;

namespace Lab6.Models
{
    public class Workouts
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Duration { get; set; }
        public required string Dificulty { get; set; }

    }
}
