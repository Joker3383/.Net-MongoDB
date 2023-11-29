using Lab6.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lab6.Repositories.Services
{
    public class NutritionService : Repository<Nutrition>
    {
        public NutritionService(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public void CreateOne(Nutrition nutrition)
        {
            CreateOneRepo(nutrition);
        }
        public void Update(FilterDefinition<Nutrition> filter, UpdateDefinition<Nutrition> update)
        {
            UpdateOneRepo(filter, update);
        }
        public void DeleteOne(Nutrition nutrition)
        {
            DeleteOneRepo(Builders<Nutrition>.Filter.Eq(nameof(nutrition.Id), nutrition.Id));
        }
        public IEnumerable<Nutrition> GetNutritions()
        {
            return GetAllRepo();
        }
        public Nutrition? GetNutritionById(ObjectId id)
        {
            return FindRepo(x => x.UserId == id).FirstOrDefault();
        }

        public List<BsonDocument> GetIndexes()
        {
            return GetIndexesRepo();
        }
        public void CreateIndex(CreateIndexModel<Nutrition> indexModel)
        {
            CreateIndexRepo(indexModel);
        }
    }
}
