using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Extensions
{
    public static class UserExtensions
    {
        public static string GetFullName(this User user)
        {
            return $"{user.Name} {user.Surname}";
        }
    }
}
