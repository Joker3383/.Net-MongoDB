using Lab6.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Repositories.Services
{
    public class WorkoutService :Repository<Workouts>
    {
        public WorkoutService(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public void CreateOne(Workouts workouts)
        {
            CreateOneRepo(workouts);
        }
        public int CreateMany(List<Workouts> workouts)
        {
            return CreateManyRepo(workouts);
        }
        public void Update(FilterDefinition<Workouts> filter, UpdateDefinition<Workouts> update)
        {
            UpdateOneRepo(filter, update);
        }
        public void DeleteOne(FilterDefinition<Workouts> filter)
        {
            DeleteOneRepo(filter);
        }
        public long DeleteMany(FilterDefinition<Workouts> filter)
        {
            return DeleteManyRepo(filter);
        }
        public IEnumerable<Workouts> GetWorkouts()
        {
            return GetAllRepo();
        }
        public Workouts? GetWorkoutsById(ObjectId id)
        {
            return FindRepo(x => x.Id == id).FirstOrDefault();
        }
        public List<BsonDocument> GetIndexes()
        {
            return GetIndexesRepo();
        }
        public void CreateIndex(CreateIndexModel<Workouts> indexModel)
        {
            CreateIndexRepo(indexModel);
        }
    }
}
