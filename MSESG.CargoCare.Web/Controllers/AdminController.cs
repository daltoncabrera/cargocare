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
using MSESG.CargoCare.Web.Models;

namespace MSESG.CargoCare.Web.Controllers
{

  public class AdminController : BaseController
  {

    public AdminController(UnitOfWork uow) : base(uow)
    {
    }

  
  
    
  }
}
