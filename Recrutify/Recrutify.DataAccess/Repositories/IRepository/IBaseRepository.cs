using System.Collections.Generic;

namespace Recrutify.DataAccess.Repositories.IRepository
{
    public interface IBaseRepository<T>
    {
        public List<T> GetAll();
    }
}
