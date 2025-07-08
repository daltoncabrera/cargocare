using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MSESG.CargoCare.Core.EFServices;
using Newtonsoft.Json;

namespace MSESG.CargoCare.Web.Handler
{
  public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
  {
    private UsuarioService _usuarioService;
    private static string? _empresa;
    private static int _empresaId;
    private static string? _cliente;
    private static int _clienteId;
    private static ClaimsPrincipal _principal;
    public AppClaimsPrincipalFactory(
        UsuarioService usuarioService,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    {
      _usuarioService = usuarioService;
    }

    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
      _principal = await base.CreateAsync(user);

      var sessData = new AppSessionData();
      sessData.UserId = user.Id;
      sessData.FullName = user.FullName;
      sessData.Email = user.Email;
      sessData.UserId = user.Id;
      sessData.Cliente = _cliente;
      sessData.ClienteId = _clienteId;
      sessData.Empresa = _empresa;
      sessData.EmpresaId = _empresaId;

      ((ClaimsIdentity)_principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FullName),
              new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(sessData)),

            });

      return _principal;
    }

    public static void  UpdateUserData(AppSessionData data)
    {
      _empresa = data.Empresa;
      _empresaId = data.EmpresaId;
      _cliente = data.Cliente;
      _clienteId = data.ClienteId;
    }

  }
}
