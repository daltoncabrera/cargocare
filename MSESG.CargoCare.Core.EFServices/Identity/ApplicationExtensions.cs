using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public static class ApplicationExtensions
    {
        public static async Task SeedData(this IApplicationBuilder app)
        {
            var ctx = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            ctx.Database.Migrate();
            var uow = app.ApplicationServices.GetRequiredService<UnitOfWork>();


            await SeedSuperAdmin(app, uow);
            await SeedEnities(app, uow);
        }

        private static async Task SeedEnities(IApplicationBuilder app, UnitOfWork uow)
        {
            var appctx = uow.AppDbContext;

            if (!appctx.Empresas.Any())
            {

                // using (var trans = appctx.Database.BeginTransaction())
                {
                    try
                    {
                        #region Filling Entities

                        var marhex = AgregarEmpresa(uow, "Marhex Soluciones SRL.");
                        var elite = AgregarEmpresa(uow, "Elite Survey Group");
                        uow.Save();

                        var pebras = CreateCliente(uow, marhex.Id, "Petroleo Caribe", "Dalton Cabrera");
                        var atlan = CreateCliente(uow, elite.Id, "Petro Athen", "Mijijo Cabrera");
                        uow.Save();

                        var rolsupervisor = await CreateRole(app, RoleType.Empresas, "Supervisor", "Usuario Supervisor");
                        var rolInspector = await CreateRole(app, RoleType.Empresas, "Inspector", "Usuario Inspector");
                        var rolGerente = await CreateRole(app, RoleType.Empresas, "Gerente", "Usuario Gerente");

                        var rolReprensentante = await CreateRole(app, RoleType.Empresas, "Representante", "Usuario Representante");

                        var user1 = await CreateUser(app, "daltoncabrera@gmail.com", "Dalton Cabrera");
                        var user2 = await CreateUser(app, "mijijo@gmail.com", "Dalton Cabrera");

                        var user3 = await CreateUser(app, "rguzman@marhexsoluciones.com", "Robert marhex");
                        var user4 = await CreateUser(app, "dcabrera@marhexsoluciones.com", "Dalton marhex");
                        var user5 = await CreateUser(app, "rguzman@elitesurvaygroup.com", "Robert elite");
                        var user6 = await CreateUser(app, "dcabrera@elitesurvaygroup.com", "Dalton elite");


                        AddUserToRole(app, uow, marhex.Id, atlan.Id, user1, rolReprensentante);
                        AddUserToRole(app, uow, marhex.Id, pebras.Id, user2, rolReprensentante);

                        AddUserToRole(app, uow, marhex.Id, 0, user3, rolInspector);
                        AddUserToRole(app, uow, elite.Id, 0, user4, rolsupervisor);

                        AddUserToRole(app, uow, marhex.Id, 0, user5, rolInspector);
                        AddUserToRole(app, uow, elite.Id, 0, user6, rolsupervisor);




                        // trans.Commit();

                        #endregion
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        /// trans.Rollback();
                    }
                }

            }

        }

        private static Cliente CreateCliente(UnitOfWork uow, int empresaId, string? nombre, string? contacto)
        {

            var repo = new ClienteRepository(uow.AppDbContext);
            var cli = new Cliente();
            cli.EmpresaId = empresaId;
            cli.Nombre = nombre;
            cli.Slug = CoreUtils.CleanInput(nombre);
            cli.Contacto = contacto;
            repo.Insert(cli);
            return cli;
        }

        private static Empresa AgregarEmpresa(UnitOfWork uow, string? nombre)
        {
            var repo = new EmpresaRepository(uow.AppDbContext);
            var emp = new Empresa();
            emp.Nombre = nombre;
            emp.Slug = CoreUtils.CleanInput(nombre);
            repo.Insert(emp);
            return emp;
        }




        private static async Task SeedSuperAdmin(IApplicationBuilder app, UnitOfWork uow)
        {
            var user = await CreateUser(app, "sadmin@esg.com", "Super Administrador");
            var role = await CreateRole(app, RoleType.Empresas, "sadmin", "Super Admin Role");
            AddUserToRole(app, uow, 0, 0, user, role);
        }

        private static async Task CreateUserWithRole(IApplicationBuilder app, UnitOfWork uow, int empresaId, int clienteId, string? mail, string? fullName, string? roleName, string? roleDesc, RoleType type = RoleType.Cliente)
        {
            var user = await CreateUser(app, mail, fullName);
            var role = await CreateRole(app, type, roleName, roleDesc);
            AddUserToRole(app, uow, empresaId, clienteId, user, role);
        }

        private static async Task<ApplicationRole> CreateRole(IApplicationBuilder app, RoleType type, string? roleName, string? roleDesc)
        {
            var roleManager = app.ApplicationServices.GetService<RoleManager<ApplicationRole>>();

            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new ApplicationRole();
                role.Name = roleName;
                role.Description = roleDesc;
                role.RoleType = type;
                await roleManager.CreateAsync(role);
            }
            return role;
        }
        private static void AddUserToRole(IApplicationBuilder app, UnitOfWork uow, int empresaId, int clienteId, ApplicationUser user, ApplicationRole role)
        {
            var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();

            var c = uow.AppDbContext;
            var appUserRole = c.UserRoles
                .FirstOrDefault(s => s.RoleId == role.Id
                                     && s.UserId == user.Id
                                     && s.ClienteId == clienteId &&
                                     s.EmpresaId == empresaId);
            if (appUserRole == null)
            {
                appUserRole = new ApplicationUserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                    EmpresaId = empresaId,
                    ClienteId = clienteId
                };

                c.UserRoles.Add(appUserRole);
                uow.Save();
                c.Entry(appUserRole).State = EntityState.Detached;
            }

        }

        private static async Task<ApplicationUser> CreateUser(IApplicationBuilder app, string? email, string? fullName)
        {
            var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
            var roleManager = app.ApplicationServices.GetService<RoleManager<ApplicationRole>>();

            var user = await userManager.FindByNameAsync(email);
            if (user == null || user.Id <= 0)
            {
                user = new ApplicationUser();
                user.Email = email;
                user.UserName = email;
                user.Activo = true;
                user.FullName = fullName;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString("D");
                await userManager.CreateAsync(user, "Aa123!");
            }

            return user;
        }
    }
}
