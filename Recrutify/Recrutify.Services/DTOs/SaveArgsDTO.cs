using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public delegate void SaveDetailsHandler(object sender, SaveArgsDTO args);

    public class SaveArgsDTO : EventArgs
    {
        public IEnumerable<Guid> Ids { get; set; }

        public StatusDTO Status { get; set; }
    }
}
