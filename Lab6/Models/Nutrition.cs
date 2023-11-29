using MongoDB.Bson;

namespace Lab6.Models
{
    public class Nutrition
    {

        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public int Intake { get; set; }
        public int CalorieCountAtDay { get; set; }
    }
}
