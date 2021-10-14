using Recrutify.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Repositories.IRepository
{
    public interface IBaseRepository<ModelType>
    {
        public List<ModelType> GetAll();
    }
}
