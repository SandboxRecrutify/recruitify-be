using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IStatusChangeEventHandler
    {
        event SaveDetailsHandler UpdateStatusByIdsAsyncComlited;

        public void UpdateStatusComplited(object sender, SaveArgsDTO e);

        public void OnUpdateStatusByIdsAsyncComlited(SaveArgsDTO e);
    }
}
