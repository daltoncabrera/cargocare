using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Web.Handler;
using MSESG.CargoCare.Web.Models;
using Newtonsoft.Json;
using HttpContext = Stimulsoft.System.Web.HttpContext;

namespace MSESG.CargoCare.Web.Controllers
{
  [Authorize]
  public class BaseController : Controller
  {
    protected UnitOfWork _unitOfWork;
    protected AppSessionData CurAppSessionData;

    public BaseController(UnitOfWork uow)
    {
      _unitOfWork = uow;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      CurAppSessionData = GetSessionData();
      Stimulsoft.Base.StiLicense.Key =
        "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHliZG/ZVfwub559uieye6DwQOHQsWZgBJ61ZP4o" +
        "WVijKQwtF9j7MBwTebcg7djc8L4i76VAlAkiaOCicZuR1OzpulVRnlibEgUr7xOba7iVhdrEQ8pk5rmM" +
        "V02cpiqJ9W4e41CnpmA+sm/WyAyjcEv+wz0W/K3Ta1aTZwVE85+doMUOcYLKFkQnIqlfgplxTR47PS+R" +
        "rw1PIHPxjlM9OEBBSnyJKVfKIPCLGvoOqMln1QIdG+FPkcYoUZKq8Pq16uaL6M3w5Qb5yyGSIEmfUx/c" +
        "DZcZFgBn3T/31wfFEIpMyT6Cw5QyjUdhr3eQYzhe2fpNMdeeu/j1vT2jnIv74lwprYN7jKW8qOQbv+yR" +
        "J35E3rY6wWCzgvUVMzPvDgxwS3Q/njhJb5a5/bz+Q7CeJ28KsZaolkjitkVAkamaktGQE5st36p7yYQX" +
        "PpSTt8n3H/TzvoCyZjZsfSgtprDF6iK4g9/Fm2ntXeTaMGOTSSSuZpIq8YFgwY+XEBkX9grbaq67/ls9" +
        "zt7qeXSv2fLH1jG1T6XcVdMrMkaqvJO0gNviJw==";

      base.OnActionExecuting(context);
    }

    protected void fillMenu()
    {
      var sessionData = GetSessionData();
      var menuItems = MenuModel.GetMenu(User.Claims.Select(s => s.Value).ToList(), sessionData);
      ViewData["Menu"] = menuItems.Where(s => s.Visible).ToList();
      ViewData["SessionData"] = sessionData;
    }

    protected bool HasClaims(params string[] claims)
    {
      var userClaims = User.Claims.Select(s => s.Value);
      return userClaims.Any(claims.Contains);
    }

    protected AppSessionData GetSessionData()
    {
      var sessionUser = User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.UserData)?.Value;
      var user = JsonConvert.DeserializeObject<AppSessionData>(sessionUser);
      return user;
    }

    protected string? GetCurUsermail()
    {
      return User?.Claims?.FirstOrDefault(s => s.Type == ClaimTypes.Name)?.Value;
    }

    protected void fillCompanies()
    {
      var companies = _unitOfWork.UsuarioService.GetEmpresaByUser(GetCurUsermail());
      ViewData["Empresas"] = companies;
    }

    protected void fillClients()
    {
      var clients = _unitOfWork.UsuarioService.GetClientByUser(GetCurUsermail());
      ViewData["Clientes"] = clients;
    }

    protected void EnsureCliente(Updateable entity)
    {
      if (entity.ClienteId.HasValue && entity.ClienteId > 0 && (entity.ClienteId != CurAppSessionData.ClienteId || entity.EmpresaId != CurAppSessionData.EmpresaId))
        throw new Exception("Entidad no pertenece al cliente actual");

    }

    protected IActionResult HandleError(Exception ex)
    {
      var result = new BadRequestResult();

      Response.StatusCode = (int)HttpStatusCode.BadRequest;
      return (result);
    }

    protected string? CleanInput(string? strIn, bool toUpper = false)
    {
      return CoreUtils.CleanInput(strIn, toUpper);
    }

    protected void FillUpdateable(Updateable entity, bool isForCliente = true)
    {
      if (entity.Id <= 0)
      {
        entity.CreatedDate = DateTime.Now;
        entity.CreatedUser = CurAppSessionData.UserId;
      }

      entity.ModifiedUser = CurAppSessionData.UserId;
      entity.ModifiedDate = DateTime.Now;

      if (isForCliente)
      {
        entity.EmpresaId = CurAppSessionData.EmpresaId;
        entity.ClienteId = CurAppSessionData.ClienteId;
      }

    }


    protected string? GetFilesDirectory()
    {

      var path = new HttpContext(this.HttpContext).Server.MapPath("wwwroot/files");
      //var curDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "wwwroot" + Path.DirectorySeparatorChar + "files";
      return path;
    }


    protected  List<string> GetConducesTpls()
    {
      var path = new HttpContext(this.HttpContext).Server.MapPath("Reportes");
      if (!string.IsNullOrWhiteSpace(path))
      {
        var files = Directory.GetFiles(path, "conduce*.mrt");
        return files.Select(s => Path.GetFileName(s)).ToList();
      }

      return null;
    }
  }
}