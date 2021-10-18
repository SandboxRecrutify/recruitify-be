using System.Collections.Generic;

namespace Recrutify.DataAccess.Repositories.IRepository
{
    public interface IBaseRepository<T>
    {
        public T Creat(T type);

        public List<T> Read();
    }
}
