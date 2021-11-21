using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class SaveArgs : EventArgs
    {
        public IEnumerable<Guid> Ids { get; set; }
    }

    public delegate void SaveDetailsHandler(object sender, SaveArgs args);
}
