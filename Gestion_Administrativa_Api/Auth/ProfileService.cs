using Gestion_Administrativa_Api.Repository;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;

namespace Gestion_Administrativa_Api.Auth
{
    public class ProfileService : IProfileService
    {

        private readonly IUserRepository _userRepository;


        public ProfileService(IUserRepository userrepository)
        {
            _userRepository = userrepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var consulta = await _userRepository.listarUsuario(context.Subject.GetSubjectId());
            var _Claims = new[] {


                            new Claim(JwtClaimTypes.Name, $"{consulta.IdUsuarioNavigation.Nombre}"),

            };

            context.IssuedClaims = _Claims.ToList();

            await Task.FromResult(0);
            return;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }


    }
    
    
}
