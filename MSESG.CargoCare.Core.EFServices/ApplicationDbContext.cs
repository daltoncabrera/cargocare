using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IMyContext
  {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
      foreach (var entity in builder.Model.GetEntityTypes())
      {
        entity.Relational().TableName = entity.DisplayName();
      }



      builder.Entity<ApplicationUserRole>().HasKey(i => new { i.EmpresaId, i.ClienteId, i.RoleId, i.UserId });
      builder.Entity<RolePermiso>().HasKey(i => new { i.RolId, i.PermisoId });
      builder.Entity<Cliente>().HasIndex(i => i.Slug).IsUnique();
      builder.Entity<Empresa>().HasIndex(i => i.Slug).IsUnique();
      base.OnModelCreating(builder);

      var dummyVar = "";
    }

    

    public DbSet<Camion> Camiones { get; set; }
    public DbSet<Planificacion> Planificaciones { get; set; }
    public DbSet<PlanificacionDespacho> PlanificacionDespachos { get; set; }
    public DbSet<PlanificacionDetalle> PlanificacionDetalles { get; set; }
    public DbSet<PlanificacionDestino> PlanificacionDestinos { get; set; }
    public DbSet<Cisterna> Cisternas { get; set; }
    public DbSet<CisternaDetalle> CisternaDetalles { get; set; }
    public DbSet<Chofer> Choferes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Componente> Componentes { get; set; }
    public DbSet<Inspeccion> Inspeciones { get; set; }
    public DbSet<InspeccionDetalle> InspeccionDetalles { get; set; }
    public DbSet<InspeccionTipo> InspeccionTipos { get; set; }
    public DbSet<Verificacion> Verificaciones { get; set; }
    public DbSet<VerificacionDetalle> VerificacionDetalles { get; set; }
    public DbSet<Sello> Sellos { get; set; }
    public DbSet<Orden> Ordenes { get; set; }
    public DbSet<OrdenDetalle> OrdenDetalles { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Terminal> Terminales { get; set; }
    public DbSet<UnidadMedida> UnidadMedidas { get; set; }
    public DbSet<UpdaterTrack> UpdaterTrackers { get; set; }
    public DbSet<Variable> Variables { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<UpdaterTrack> UpdateTracks { get; set; }
    public DbSet<RolePermiso> ApplicationRolPermisos { get; set; }
    public DbSet<Correo> Correos { get; set; }
    public DbSet<EmpresaSetting> EmpresaSettings { get; set; }
    public DbSet<ProductosCliente> ProductosClientes { get; set; }
    public DbSet<Precarga> Precargas { get; set; }
    public DbSet<PrecargaDetalle> PrecargaDetalles { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Observacion> Observaciones { get; set; }


  }
}
