using Lab6.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Lab6.Repositories.Services
{
    public class UserService : Repository<Users>
    {
        public UserService(IMongoDatabase database, string collectionName) : base(database, collectionName) { }

        public void CreateOne(Users user)
        {
            CreateOneRepo(user);
        }
        public int CreateMany(List<Users> users)
        {
            return CreateManyRepo(users);
        }
        public void Update(FilterDefinition<Users> filter, UpdateDefinition<Users> update)
        {
            UpdateOneRepo(filter, update);
        }
        public void DeleteOne(FilterDefinition<Users> filter)
        {
            DeleteOneRepo(filter);
        }
        public long DeleteMany(FilterDefinition<Users> filter)
        {
            return DeleteManyRepo(filter);
        }
        public IEnumerable<Users> GetUsers()
        {
            return GetAllRepo();
        }
        public Users? GetUserById(ObjectId id)
        {
            return FindRepo(x => x.Id == id).FirstOrDefault();
        }

        public List<BsonDocument> GetIndexes()
        {
            return GetIndexesRepo();
        }
        public void CreateIndex(CreateIndexModel<Users> indexModel)
        {
            CreateIndexRepo(indexModel);
        }
    }
}
