using Lab6.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Lab6.Repositories.Services
{
    public class LogsService : Repository<Logs>
    {
        public LogsService(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public void CreateOne(Logs log)
        {
            CreateOneRepo(log);
        }
        public void Update(FilterDefinition<Logs> filter, UpdateDefinition<Logs> update)
        {
            UpdateOneRepo(filter, update);
        }
        public void DeleteOne(FilterDefinition<Logs> filter)
        {
            DeleteOneRepo(filter);
        }
        public long DeleteMany(FilterDefinition<Logs> filter)
        {
            return DeleteManyRepo(filter);
        }
        public IEnumerable<Logs> GetLogs()
        {
            return GetAllRepo();
        }
        public Logs? GetLogById(ObjectId id)
        {
            return FindRepo(x => x.Id == id).FirstOrDefault();
        }
        public Logs? GetLogByUserId(ObjectId id)
        {
            return FindRepo(x => x.UserId == id).FirstOrDefault();
        }
        public Logs? GetLogByWorkoutId(ObjectId id)
        {
            return FindRepo(x => x.WorkoutId == id).FirstOrDefault();
        }
        public Logs? GetLogByUserIdAndWorkoutId(ObjectId userId, ObjectId WorkoutId)
        {
            return FindRepo(x => x.UserId == userId && x.WorkoutId == WorkoutId).FirstOrDefault();
        }
        public List<BsonDocument> GetIndexes()
        {
            return GetIndexesRepo();
        }
        public void CreateIndex(CreateIndexModel<Logs> indexModel)
        {
            CreateIndexRepo(indexModel);
        }
    }
}
