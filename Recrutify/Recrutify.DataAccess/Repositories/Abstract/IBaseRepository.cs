using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IBaseRepository<TDocument>
    {
        Task CreateAsync(TDocument item);

        Task<List<TDocument>> GetAllAsync();
    }
}
