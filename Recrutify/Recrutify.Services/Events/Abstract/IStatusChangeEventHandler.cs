using System.Threading.Tasks;

namespace Recrutify.Services.Events.Abstract
{
    public interface IStatusChangeEventHandler
    {
        public Task UpdateCandidatesStatusesAsync(UpdateStatusEventArgs e);
    }
}
