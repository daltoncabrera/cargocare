using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ApplicationUserRole: IdentityUserRole<int>
    {
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
    }
}
