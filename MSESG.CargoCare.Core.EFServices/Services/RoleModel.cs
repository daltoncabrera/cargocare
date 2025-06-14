using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;

namespace MSESG.CargoCare.Core.EFServices
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }
        public int ClienteId { get; set; }
        public int EmpresaId { get; set; }
        public  RoleType RoleType { get; set; }
        public bool Checked { get; set; }
        public bool OldChecked { get; set; }
        public string Description { get; internal set; }
    }

    public class EmpresaRoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleModel> Roles { get; set; }
        public List<ClienteRolModel> Clientes { get; set; }
    }

    public class ClienteRolModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<RoleModel> Roles { get; set; }
    }

}
