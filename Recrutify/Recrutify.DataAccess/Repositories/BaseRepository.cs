using MongoDB.Driver;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.DataAccess.Repositories
{
    public abstract class BaseRepository<ModelType>
        : IBaseRepository<ModelType> where ModelType : IDataModel
    {
        private readonly IMongoCollection<ModelType> _collection;
        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<ModelType>(collectionName);
        }

        public List<ModelType> GetAll()
        {
            return _collection.Find(x => true).ToList();
        }
    }
}
