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

        IQueryable<TDocument> Get();

        Task<TDocument> GetAsync(Guid id);

        Task<List<TDocument>> GetAllAsync();

        Task UpdateAsync(TDocument item);

        Task DeleteAsync(Guid id);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> ExistsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
