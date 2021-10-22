﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess.Models
{
    public class Staff
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public Role Role { get; set; }
    }
}
