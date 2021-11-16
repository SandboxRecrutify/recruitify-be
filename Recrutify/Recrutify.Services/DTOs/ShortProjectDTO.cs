﻿using System;

namespace Recrutify.Services.DTOs
{
    public class ShortProjectDTO
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartRegistrationDate { get; set; }

        public DateTime EndRegistrationDate { get; set; }

        public string Description { get; set; }
    }
}
