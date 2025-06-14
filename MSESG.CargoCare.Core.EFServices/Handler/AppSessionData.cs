using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Core.EFServices
{
    public class AppSessionData
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int EmpresaId { get; set; }
        public string Empresa { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public bool Active { get; set; } = false;
        public bool HasToChangePassword { get; set; } = false;
    }
}
