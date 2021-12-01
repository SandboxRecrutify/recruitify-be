using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Validators
{
    public class CreateProjectValidator : BaseProjectValidator<CreateProjectDTO>
    {
        public CreateProjectValidator(IUserRepository userRepository)
            : base(userRepository)
        {
        }
    }
}
