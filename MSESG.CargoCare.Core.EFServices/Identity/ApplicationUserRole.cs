using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ApplicationUserRole: IdentityUserRole<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
    }
}
