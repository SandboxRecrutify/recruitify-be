using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IBaseRepository<TDocument>
    {
        Task CreateAsync(TDocument item);

        Task<TDocument> GetAsync(Guid id);

        Task<List<TDocument>> GetAllAsync();

        IQueryable<TDocument> Get();

        Task UpdateAsync(TDocument item);

        Task DeleteAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);

        Task<bool> ExistsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
