using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.Providers
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}
