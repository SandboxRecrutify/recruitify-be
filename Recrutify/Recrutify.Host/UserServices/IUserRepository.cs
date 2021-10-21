using Recrutify.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Recrutify.Host
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string login, string password);

        User FindBySubjectId(string subjectId);

        User FindByLogin(string login);
       
    }
}
