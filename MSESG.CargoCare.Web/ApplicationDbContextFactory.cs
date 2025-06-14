using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using MSESG.CargoCare.Core.EFServices;

namespace MSESG.CargoCare.Web
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContextFactory()
        {
        }


        public ApplicationDbContext CreateDbContext(string[] args)
        {
            return Program.BuildWebHost(args)
                .Services.GetRequiredService<ApplicationDbContext>();
        }


    }
}
