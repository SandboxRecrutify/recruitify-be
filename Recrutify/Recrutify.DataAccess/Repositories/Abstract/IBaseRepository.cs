using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IBaseRepository<TDocument>
    {
        Task CreateAsync(TDocument item);

        Task<TDocument> GetAsync(Guid id);

        Task<List<TDocument>> GetAllAsync();

        Task UpdateAsync(TDocument item);

        Task DeleteAsync(Guid id);
    }
}
