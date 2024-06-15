using Gestion_Administrativa_Api.Repository;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Gestion_Administrativa_Api.Auth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userrepository)
        {
            _userRepository = userrepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var consulta = await _userRepository.ExisteUsuario(context.UserName, context.Password);

            if (consulta == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                return;
            }
            context.Result = new GrantValidationResult(

                              subject: context.UserName.ToString(),
                              authenticationMethod: "token"

                              );

            return;
        }
    }
}