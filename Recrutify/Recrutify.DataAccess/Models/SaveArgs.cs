using System;
using System.Collections.Generic;

namespace Recrutify.DataAccess.Models
{
    public delegate void SaveDetailsHandler(object sender, SaveArgs args);

    public class SaveArgs : EventArgs
    {
        public IEnumerable<Guid> Ids { get; set; }

        public Status Status { get; set; }
    }
}
