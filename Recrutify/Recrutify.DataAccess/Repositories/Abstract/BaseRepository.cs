using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public abstract class BaseRepository<TDocument>
        : IBaseRepository<TDocument>
        where TDocument : IDataModel
    {
        private readonly IMongoDatabase _database;
        private readonly FilterDefinitionBuilder<TDocument> _filterBuilder;

        protected BaseRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(options.Value.ConnectionString).DatabaseName);
            _filterBuilder = Builders<TDocument>.Filter;
        }

        public Task CreateAsync(TDocument item)
        {
           return GetCollection().InsertOneAsync(item);
        }

        public Task<List<TDocument>> GetAllAsync()
        {
            var filter = _filterBuilder.Empty;
            return GetCollection().Find(filter).ToListAsync();
        }

        private IMongoCollection<TDocument> GetCollection()
        {
            return _database.GetCollection<TDocument>(typeof(TDocument).Name);
        }
    }
}
