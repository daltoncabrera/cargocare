using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Web.Handler;
using MSESG.CargoCare.Web.Models;

namespace MSESG.CargoCare.Web.Controllers
{

  public class HomeController : BaseController
  {
    private readonly SignInManager<ApplicationUser> _signInManager;

    public HomeController(UnitOfWork uow, SignInManager<ApplicationUser> signInManager) : base(uow)
    {
      _signInManager = signInManager;
    }

    [Route("")]
    [Route("Home")]
    [Route("Home/Index")]
    [Route("Home/{empresa}/{cliente}")]
    [Route("App/{empresa}/{cliente}")]
    public async Task<IActionResult> Index(string? empresa, string? cliente)
    {
      var sesionData = GetSessionData();
      if (sesionData  == null)
      {
        return RedirectToAction(nameof(AccountController.Exit), "Account", new { returnUrl = "/" });
      }


      var usermail = GetCurUsermail();
      var result = _unitOfWork.UsuarioService.GetClientByUser(usermail);
      var curCliente = string.IsNullOrWhiteSpace(cliente)
          || string.IsNullOrEmpty(empresa) ? null
          : result.FirstOrDefault(s => s.ClienteSlug == cliente);

     
      if (!User.IsInRole("sadmin") && curCliente == null)
      {

        sesionData.EmpresaId = 0;
        sesionData.Empresa = "";
        sesionData.ClienteId = 0;
        sesionData.Cliente = "";
        ViewData["CurCliente"] = null;
        ViewData["baseurl"] = "";
        var user = _unitOfWork.UsuarioService.GetApplicationUser(sesionData.UserId);
        AppClaimsPrincipalFactory.UpdateUserData(sesionData);
        await _signInManager.RefreshSignInAsync(user);
        return RedirectToAction(nameof(HomeController.SelectClient), "Home");
      }
      else
      {
        sesionData.EmpresaId = curCliente?.EmpresaId ?? 0;
        sesionData.Empresa = curCliente?.EmpresaName;
        sesionData.ClienteId = curCliente?.ClientId ?? 0;
        sesionData.Cliente = curCliente?.ClientName;
        
        ViewData["CurCliente"] = curCliente;
        ViewData["UserClaims"] = string.Join(",", User.Claims.Where(s => s.Type == ClaimTypes.Role).Select(s => s.Value));
        ViewData["baseurl"] = curCliente != null ? string.Format("App/{0}/{1}", empresa, cliente) : "";
  
        var user = _unitOfWork.UsuarioService.GetApplicationUser(sesionData.UserId);
        AppClaimsPrincipalFactory.UpdateUserData(sesionData);
        await _signInManager.RefreshSignInAsync(user);
        HttpContext.User = await _unitOfWork.SignInManager.CreateUserPrincipalAsync(user);
      }
      
      fillMenu();
      fillClients();
      fillCompanies();
      return View();


    }



    [HttpGet]
    [Authorize]
    public IActionResult SelectClient()
    {
      var usermail = GetCurUsermail();
      var result = _unitOfWork.UsuarioService.GetClientByUser(usermail);
      if (result.Count == 1)
      {
        var uC = result.FirstOrDefault();
        var url = string.Format("/App/{0}/{1}/", uC.EmpresaSlug, uC.ClienteSlug);
        return Redirect(url);
      }

      return View(result);
    }



    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Error()
    {
      return View();
    }


  }
}
