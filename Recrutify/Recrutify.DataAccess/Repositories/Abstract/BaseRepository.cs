using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public abstract class BaseRepository<TDocument>
        : IBaseRepository<TDocument>
        where TDocument : IDataModel
    {
        protected readonly FilterDefinitionBuilder<TDocument> _filterBuilder;
        private readonly IMongoDatabase _database;

        protected BaseRepository(IOptions<MongoSettings> options)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
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

        public Task<TDocument> GetByIdAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(u => u.Id, id);
            return GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(TDocument item)
        {
            var filter = _filterBuilder.Eq(e => e.Id, item.Id);
            return GetCollection().ReplaceOneAsync(filter, item);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(e => e.Id, id);
            await GetCollection().DeleteOneAsync(filter);
        }

        protected IMongoCollection<TDocument> GetCollection()
        {
            return _database.GetCollection<TDocument>(typeof(TDocument).Name);
        }
    }
}
