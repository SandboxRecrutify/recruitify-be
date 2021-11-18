using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        protected readonly FilterDefinitionBuilder<TDocument> _filterBuilder;
        private readonly IMongoDatabase _database;

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

        public IQueryable<TDocument> Get()
        {
            return GetCollection().AsQueryable<TDocument>();
        }

        public Task<TDocument> GetAsync(Guid id)
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

        public async Task<bool> ExistsAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(e => e.Id, id);
            var result = await GetCollection().Find(filter).CountDocumentsAsync();
            return result != 0;
        }

        public async Task<bool> ExistsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            var filter = _filterBuilder.In(u => u.Id, ids);
            var foundCount = await GetCollection().Find(filter).CountDocumentsAsync();
            return foundCount == ids.Count();
        }

        protected IMongoCollection<TDocument> GetCollection()
        {
            return _database.GetCollection<TDocument>(typeof(TDocument).Name);
        }
    }
}
