using MongoDB.Driver;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.DataAccess.Repositories
{
    public abstract class BaseRepository<T>
        : IBaseRepository<T>
        where T : IDataModel
    {
        private readonly IMongoCollection<T> collection;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            this.collection = database.GetCollection<T>(collectionName);
        }

        public List<T> GetAll()
        {
            return this.collection.Find(x => true).ToList();
        }
    }
}
