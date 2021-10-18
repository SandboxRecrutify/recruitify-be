using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.IRepository;
using Recrutify.Host.Configuration;

namespace Recrutify.DataAccess.Repositories
{
    public abstract class BaseRepository<T>
        : IBaseRepository<T>
        where T : IDataModel
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(MongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>("T");
        }

        public T Creat(T type)
        {
            _collection.InsertOne(type);
            return type;
        }

        public List<T> Read() =>
            _collection.Find(type => true).ToList();
    }
}
