﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Core.EFServices
{
  public class ApplicationClaimsPrincipal : ClaimsPrincipal
  {
    public ApplicationClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
    {

    }

    public long UserId
    {
      get { return long.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
    }
  }
}
