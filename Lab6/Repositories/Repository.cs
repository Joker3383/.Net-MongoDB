using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Lab6.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }
        public void CreateOneRepo(T document)
        {
            _collection.InsertOne(document);
        }
        public int CreateManyRepo(List<T> documents)
        {
            _collection.InsertMany(documents);
            return documents.Count();
        }
        public void UpdateOneRepo(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            _collection.UpdateOne(filter,update);
        }
        public void DeleteOneRepo(FilterDefinition<T> filter)
        {
            _collection.DeleteOne(filter);
        }
        public long DeleteManyRepo(FilterDefinition<T> filter)
        {
           var result =  _collection.DeleteMany(filter);
            return result.DeletedCount;
        }
        public IEnumerable<T> GetAllRepo()
        {
            return _collection.Find("{}").ToList();
        }
        public IEnumerable<T> FindRepo(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).ToList();
        }
        public List<BsonDocument> GetIndexesRepo()
        {
            return _collection.Indexes.List().ToList();
        }
        public void CreateIndexRepo(CreateIndexModel<T> indexModel)
        {
            _collection.Indexes.CreateOne(indexModel);
        }
    }
}
