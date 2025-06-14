using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSESG.CargoCare.Core.EFServices.Services;

namespace MSESG.CargoCare.Core.EFServices
{
  public class UsuarioService
  {
    private ApplicationDbContext _ctx;
    private UsuarioRepository _usuarioRepository;
    private RoleRepository _roleRepository;
    private UserRoleRepository _userRoleRepository;
    private RolePermisoRepository _rolePermisoRepository;

    public UsuarioService(
        ApplicationDbContext ctx,
        UsuarioRepository usuarioRepository,
        RoleRepository roleRepository,
        UserRoleRepository userRoleRepository,
        RolePermisoRepository rolePermisoRepository)
    {
      _ctx = ctx;
      _usuarioRepository = usuarioRepository;
      _roleRepository = roleRepository;
      _userRoleRepository = userRoleRepository;
      _rolePermisoRepository = rolePermisoRepository;
    }


    #region roles
    public ApplicationRole GetRolById(int id)
    {
      return _roleRepository.All.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<ApplicationRole> GetRoles()
    {
      return _roleRepository.All;
    }

    public void UpdateRole(int id, ApplicationRole rol)
    {
      var oldRol = GetRolById(id);
      if (oldRol == null)
        throw new Exception("Invalid rol");

      throwIfRolExist(rol);
      oldRol.Name = rol.Name;
      oldRol.Description = rol.Description;
      oldRol.NormalizedName = rol.NormalizedName;

      _roleRepository.Update(oldRol);
      _ctx.SaveChanges();
      return;
    }

    public void DeleteRole(int id)
    {
      var rol = GetRolById(id);
      if (rol == null)
        throw new Exception("Invalid rol");

      _roleRepository.Delete(rol);
      _ctx.SaveChanges();
    }

    public void InsertRol(ApplicationRole rol)
    {
      throwIfRolExist(rol);
      _roleRepository.Insert(rol);
      _ctx.SaveChanges();
    }

    public ApplicationUser GetApplicationUser(int userId)
    {
      return _ctx.Users.FirstOrDefault(s => s.Id == userId);
    }

    private void throwIfRolExist(ApplicationRole role)
    {
      var nName = role.NormalizedName;
      var oldRol = _roleRepository.All.FirstOrDefault(s => s.NormalizedName == nName);
      if (oldRol != null)
      {
        if (oldRol.Id != role.Id)
          throw new Exception("Rol exist this name");

      }
    }
    #endregion

    #region Usuario
    #endregion

    public IEnumerable<UsuarioModel> GetUsuarios()
    {
      var usuarios = _usuarioRepository.All;
      var result = new List<UsuarioModel>();
      foreach (var u in usuarios)
      {
        result.Add(new UsuarioModel
        {
          Id = u.Id,
          Email = u.Email,
          FullName = u.FullName,
          Phone = u.PhoneNumber,
          Activo = u.Activo
        });
      }

      return result;
    }

    private UsuarioModel GetUserModel(ApplicationUser u)
    {
      var t = u != null ? (from r1 in _ctx.Roles
                           from r in _ctx.Roles
                           join ur in _ctx.UserRoles.Where(s => s.UserId == u.Id) on r.Id equals ur.RoleId into urg
                           from lr in urg.DefaultIfEmpty()
                           select new
                           {
                             RoleName = r.Description,
                             RoleId = r.Id,
                             Checked = lr != null,
                             OldChecked = lr != null,
                             RoleType = r.RoleType,
                             EmpresaId = lr != null ? lr.EmpresaId : 0,
                             ClienteId = lr != null ? lr.ClienteId : 0,
                           }).ToList() : null;


      var role = _ctx.Roles;

      var um = new UsuarioModel();
      um.Id = u?.Id ?? 0;
      um.Email = u?.Email;
      um.Phone = u?.PhoneNumber;
      um.FullName = u?.FullName;
      um.Activo = u?.Activo ?? false;
      var uroles = new List<EmpresaRoleModel>();

      var eq = from e in _ctx.Empresas
               join c in _ctx.Clientes on e.Id equals c.EmpresaId into g
               select new
               {
                 Empresa = e,
                 Clientes = g
               };

      foreach (var e in eq.ToList())
      {
        var evm = new EmpresaRoleModel();
        evm.Id = e.Empresa.Id;
        evm.Name = e.Empresa.Nombre;
        evm.Roles = new List<RoleModel>();
        evm.Clientes = new List<ClienteRolModel>();

        foreach (var i in role.Where(s => s.RoleType == RoleType.Empresas && s.Name != "sadmin"))
        {
          var cheked = t != null && t.Any(r => r.EmpresaId == e.Empresa.Id && r.RoleId == i.Id && r.Checked);

          evm.Roles.Add(new RoleModel
          {
            RoleId = i.Id,
            RoleName = i.Name,
            Description = i.Description,
            ClienteId = 0,
            EmpresaId = e.Empresa.Id,
            UserId = um.Id,
            Checked = cheked,
            RoleType = RoleType.Empresas,
            OldChecked = cheked
          });
        }

        foreach (var c in e.Clientes)
        {
          var cli = new ClienteRolModel();
          cli.Id = c.Id;
          cli.name = c.Nombre;
          cli.Roles = new List<RoleModel>();
          foreach (var i in role.Where(s => s.RoleType == RoleType.Cliente))
          {
            var ccheked = t != null && t.Any(x => x.ClienteId == c.ClienteId && x.RoleId == i.Id && x.Checked);

            cli.Roles.Add(new RoleModel
            {
              RoleId = i.Id,
              ClienteId = cli.Id,
              EmpresaId = e.Empresa.Id,
              UserId = um.Id,
              RoleName = i.Name,
              Description = i.Description,
              Checked = ccheked,
              RoleType = RoleType.Empresas,
              OldChecked = ccheked
            });
          }

          evm.Clientes.Add(cli);
        }
        uroles.Add(evm);
      }

      um.Roles = uroles;

      var saminRole = _ctx.Roles.FirstOrDefault(s => s.Name == "sadmin");
      if (saminRole != null)
      {
        var scheck = t != null && t.Any(x => x.ClienteId == 0 && x.EmpresaId == 0 && x.RoleId == saminRole.Id && x.Checked);
        um.Sadmin = new RoleModel
        {
          RoleId = saminRole.Id,
          RoleName = saminRole.Name,
          Description = saminRole.Description,
          ClienteId = 0,
          EmpresaId = 0,
          UserId = um.Id,
          Checked = scheck,
          RoleType = RoleType.Empresas,
          OldChecked = scheck
        };
      }

      return um;
    }

    public void EliminarUsuario(int id)
    {
      var user = _ctx.Users.First(s => s.Id == id);
      if (user != null)
      {
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
      }
    }

    public UsuarioModel GetUsuarioById(int id, bool ThrowEmpty = true)
    {
      var usuario = _ctx.Users.FirstOrDefault(s => s.Id == id);
      if (usuario == null && ThrowEmpty)
        throw new Exception("Usuarion no encontrado");

      return GetUserModel(usuario);
    }


    public void UpdateUsuario(int id, UsuarioModel usuario)
    {
      SaveUsuario(usuario);
    }

    public void DesativeUsuario(int id)
    {
      var oldUser = _ctx.Users.FirstOrDefault(s => s.Id == id);
      oldUser.Activo = false;
      _ctx.SaveChanges();
    }

    public ApplicationUser SaveUsuario(UsuarioModel vm)
    {
      var exisingUser = _ctx.Users.FirstOrDefault(s => s.Email == vm.Email);
      if (exisingUser == null)
        throw new Exception("error guardando usuario");

      exisingUser.Activo = vm.Activo;
      exisingUser.FullName = vm.FullName;
      exisingUser.PhoneNumber = vm.Phone;
      exisingUser.Activo = vm.Activo;
      _ctx.SaveChanges();
      var rolesEmpresa = vm.Roles.SelectMany(s => s.Roles);
      var rolesCliente = vm.Roles.SelectMany(r => r.Clientes.SelectMany(s => s.Roles));
      saveUserRole(vm.Sadmin, exisingUser);
      foreach (var r in rolesEmpresa)
      {
        saveUserRole(r, exisingUser);
      }

      foreach (var r in rolesCliente)
      {
        saveUserRole(r, exisingUser);
      }

      return exisingUser;
    }


    public List<UserClientModel> GetClientByUser(string usermail)
    {
      var result = new List<UserClientModel>();
      var roles = (from u in _ctx.Users
                   from ur in _ctx.UserRoles
                   from r in _ctx.Roles
                   where u.Email == usermail
                         && u.Id == ur.UserId
                         && ur.RoleId == r.Id
                   select new
                   {
                     ur.EmpresaId,
                     ur.ClienteId,
                     ur.UserId,
                     ur.RoleId,
                     r.RoleType,
                     r.Description,
                     r.Name
                   }).ToList();

      if (roles.Any(s => s.Name == "sadmin"))
      {
        result = (from c in _ctx.Clientes
                  from e in _ctx.Empresas
                  where c.EmpresaId == e.Id
                  select new UserClientModel
                  {
                    EmpresaId = e.Id,
                    EmpresaName = e.Nombre,
                    EmpresaSlug = e.Slug,
                    ClientId = c.Id,
                    ClientName = c.Nombre,
                    ClienteSlug = c.Slug,
                    Logo = c.Logo ?? "cliente.png"
                  }).ToList();

      }
      else
      {
        if (roles.Any(s => s.RoleType == RoleType.Empresas))
        {
          var ce = from r in roles
                   from c in _ctx.Clientes.ToList()
                   from e in _ctx.Empresas.ToList()
                   where r.EmpresaId == c.EmpresaId
                     && c.EmpresaId == e.Id
                     && r.RoleType == RoleType.Empresas
                   select new UserClientModel
                   {
                     EmpresaId = e.Id,
                     EmpresaName = e.Nombre,
                     EmpresaSlug = e.Slug,
                     ClientId = c.Id,
                     ClientName = c.Nombre,
                     Logo = c.Logo ?? "cliente.png",
                     ClienteSlug = c.Slug
                   };

          result = ce.ToList();
        }

        var cli = from r in roles.Where(s => s.RoleType == RoleType.Cliente)
                  from c in _ctx.Clientes
                  from e in _ctx.Empresas
                  where r.ClienteId == c.Id
                  && c.EmpresaId == e.Id
                  select new UserClientModel
                  {
                    EmpresaId = e.Id,
                    EmpresaName = e.Nombre,
                    EmpresaSlug = e.Slug,
                    ClientId = c.Id,
                    Logo = c.Logo ?? "cliente.png",
                    ClientName = c.Nombre,
                    ClienteSlug = c.Slug
                  };

        result.AddRange(cli);

      }

      return result.Distinct().ToList();
    }

    public List<Empresa> GetEmpresaByUser(string usermail)
    {
      var result = from u in _ctx.Users
                   from e in _ctx.Empresas
                   from r in _ctx.UserRoles
                   where u.Email == usermail
                         && u.Id == r.RoleId
                         && r.EmpresaId == e.Id
                   select e;

      return result.Distinct().ToList();
    }

    private void saveUserRole(RoleModel re, ApplicationUser user)
    {
      var existinRol = _ctx.UserRoles.FirstOrDefault(s =>
        s.RoleId  == re.RoleId
        && s.UserId == re.UserId
        && s.ClienteId == re.ClienteId 
        && s.EmpresaId == re.EmpresaId);

      if (existinRol != null && !re.Checked)
      {
        _ctx.Database.ExecuteSqlCommand($" delete from aspnetuserroles where EmpresaId =  {existinRol.EmpresaId}  and RoleId =  {existinRol.RoleId}  and userId = {existinRol.UserId} and ClienteId = {existinRol.ClienteId}");
      }
      else if (existinRol == null && re.Checked)
      {
        _ctx.UserRoles.Add(new ApplicationUserRole()
        {
          EmpresaId = re.EmpresaId,
          ClienteId = re.ClienteId,
          RoleId = re.RoleId,
          UserId = user.Id
        });
        _ctx.SaveChanges();
      }
      
    }

    public Task<IList<string>> GetUserRoles(int userId, int cliente)
    {
      IList<string> result = new List<string>();

      var userR = from ur in _ctx.UserRoles
                  from r in _ctx.Roles
                  where ur.RoleId == r.Id
                     && ur.UserId == userId
                  select new { UserRole = ur, Role = r };

      var samin = userR.FirstOrDefault(s => s.Role.Name == "sadmin");

      if (samin != null)
        result.Add(samin.Role.Name);

      var empRoles = userR.Where(s => s.Role.RoleType == RoleType.Empresas);
      foreach (var eRol in empRoles)
      {
        result.Add(eRol.Role.Name);
      }

      if (cliente > 0)
      {
        var clientRoles = userR.Where(s => s.UserRole.ClienteId == cliente).Select(s => s.Role.Name);
        foreach (var clientRole in clientRoles)
        {
          result.Add(clientRole);
        }
      }

      return Task.FromResult(result);
    }
  }
}
