using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Role> Roles { get; set; }
    }
}
