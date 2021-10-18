using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.IRepository;

namespace Recrutify.DataAccess.Repositories
{
    public abstract class BaseRepository<T>
        : IBaseRepository<T>
        where T : IDataModel
    {
        protected readonly IMongoCollection<T> collection;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            collection = database.GetCollection<T>(collectionName);
        }

        public List<T> GetAll()
        {
            return collection.Find(x => true).ToList();
        }
    }
}
