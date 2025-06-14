using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MSESG.CargoCare.Core.EFServices
{
   public class ApplicationUserManager : UserManager<ApplicationUser>
   {
       private UsuarioService _usuarioService;
       private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUserManager(IHttpContextAccessor httpContextAccessor,UsuarioService usuarioService, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _usuarioService = usuarioService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var sData = _httpContextAccessor.HttpContext.Session.Get<AppSessionData>(AppSessionExtensions
                .SESSION_KEY_USER_DATA);

            var cliente = sData?.ClienteId ?? 0;
            var roles = _usuarioService.GetUserRoles(user.Id, cliente);
            return roles;
        }
    }
}
