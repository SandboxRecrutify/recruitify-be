using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutify.Host
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (userRepository.ValidateCredentials(context.UserName, context.Password))
            {
                var user = userRepository.FindByLogin(context.UserName);
                context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
            }

            return Task.FromResult(0);
        }
    }
}
