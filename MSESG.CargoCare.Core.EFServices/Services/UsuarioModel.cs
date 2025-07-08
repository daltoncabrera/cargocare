using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSESG.CargoCare.Core.EFServices;

namespace MSESG.CargoCare.Core.EFServices
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public bool Activo { get; set; }

        public IEnumerable<EmpresaRoleModel> Roles { get; set; }
        public RoleModel Sadmin { get; set; }
        public string? TempPassword { get; set; }
    }
}
        