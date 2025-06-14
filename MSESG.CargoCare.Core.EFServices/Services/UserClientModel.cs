using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices.Services
{
    public class UserClientModel 
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaName { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserMail { get; set; }
        public string ClienteSlug { get; set; }
        public string EmpresaSlug { get; set; }
        public string Logo { get; internal set; }
    public string UserClaims { get; set; }

    public override bool Equals(object obj)
        {
            var x = this;
            var y = obj as UserClientModel;

            if (obj == null)
                return false;

            return x.EmpresaId == y.EmpresaId
                   && x.EmpresaName == y.EmpresaName
                   && x.EmpresaSlug == y.EmpresaSlug
                   && x.ClientId == y.ClientId
                   && x.ClientName == y.ClientName
                   && x.ClienteSlug == y.ClienteSlug
                   && x.UserId == y.UserId
                   && x.UserFullName == y.UserFullName
                   && x.UserMail == y.UserMail;
        }

        public override int GetHashCode()
        {
            var obj = this;
            return obj.EmpresaId + obj.ClientId + obj.UserId;
        }

    }
}
