using MongoDB.Driver;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Repositories
{
    public abstract class BaseRepository<ModelType>
        : IBaseRepository<ModelType> where ModelType : BaseModel
    {
        private readonly IMongoCollection<ModelType> _collection;
        public List<ModelType> GetAll()
        {
            return _collection.Find(car => true).ToList();
        }
        
    }
}
