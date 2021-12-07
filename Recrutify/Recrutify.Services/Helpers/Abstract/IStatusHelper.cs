using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Helpers.Abstract
{
    public interface IStatusHelper
    {
        Status GetStatusUp(Status status);

        Status GetStatusDown(Status status);
    }
}
