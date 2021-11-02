﻿using System;
using System.Collections.Generic;

namespace Recrutify.Services.DTOs
{
    public class CandidateCreateDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public EnglishLevelDTO EnglishLevel { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<ContactDTO> Contacts { get; set; }

        public LocationDTO Location { get; set; }

        public List<CandidatePrimarySkillDTO> PrimarySkills { get; set; }

        public List<int> BestTimeToConnect { get; set; }

        public bool GoingToExadel { get; set; }

        public string CurrentJob { get; set; }

        public string Certificates { get; set; }

        public string AdditionalQuestions { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
