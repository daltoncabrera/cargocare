using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MSESG.CargoCare.Web.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<ExternalLoginProvider> OtherLogins { get; set; }
    }

    public class ExternalLoginProvider
    {
        public string AuthenticationScheme { get; set; }
        public string DisplayName { get; set; }
    }
}
