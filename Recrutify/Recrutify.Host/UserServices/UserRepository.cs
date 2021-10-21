using Recrutify.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutify.Host
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>
        {
            new User
            {
                Id = "123",
                UserName = "log@mail.ru",
                Password = "1234",
                Name = "Ilya",
                Surname ="Hamich",
                Salt = "adasdq",
                Roles = new List<Role>{Role.Admin}
            },
            new User
            {
                Id = "124",
                UserName = "log1@mail.ru",
                Password = "qwer",
                Name = "Vasya",
                Surname ="Pupkin",
                Salt = "adasdq",
                Roles = new List<Role>{Role.Manager}
            }
        };
        public User FindByLogin(string login)
        {
            return users.FirstOrDefault(x => x.UserName.Equals(login, StringComparison.OrdinalIgnoreCase));
        }

        public User FindBySubjectId(string subjectId)
        {
           return  users.FirstOrDefault(x => x.Id == subjectId);           
        }

        public bool ValidateCredentials(string login, string password)
        {
            var user = FindByLogin(login);
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }
    }
}
