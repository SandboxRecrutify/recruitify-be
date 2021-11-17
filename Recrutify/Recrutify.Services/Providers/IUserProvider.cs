using System;

namespace Recrutify.Services.Providers
{
    public interface IUserProvider
    {
        Guid GetUserId();

        string GetUserName();
    }
}
