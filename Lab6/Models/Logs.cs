using MongoDB.Bson;
using MongoDB.Driver;

namespace Lab6.Models
{
    public class Logs
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId WorkoutId { get; set; }
        public DateOnly Date { get; set; }
        public int CountOfMonth { get; set; }

    }
}
