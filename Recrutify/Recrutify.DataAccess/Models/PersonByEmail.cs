using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class PersonByEmail
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
