using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using MSESG.CargoCare.Core.EFServices;

namespace MSESG.CargoCare.Web.Models
{

  public static class StringExtension
  {
    public static bool OrEquals(this string? value, params string[] arg)
    {
      return arg.Contains(value);
    }

    public static bool OrContains(this string? value, params string[] arg)
    {
      var result = false;
      foreach (var a in arg)
      {
        if (value.Contains(a))
          result = true;
      }
      return result;
    }

    public static bool AndContains(this string? value, params string[] arg)
    {
      var result = false;
      foreach (var a in arg)
      {
        if (value.Contains(a))
        {
          result = true;
        }
        else
        {
          result = false;
          break;
        }
      }

      return result;
    }

  }

  public class MenuModel
  {
    public MenuModel()
    {
      //this.ChildMenuList = new List<MenuModel>();
    }
    public string? Name { get; set; }
    public string? Caption { get; set; }
    public List<string> Roles { get; set; }
    public bool Visible { get; set; }
    public string? CSSClass { get; set; }
    public string? ItemCssOpen { get; set; } = "";
    public string? ItemCssActive { get; set; } = "";
    public List<MenuModel> ChildMenuList { get; set; }

    public static List<MenuModel> GetMenu(IList<string> claims, AppSessionData sessionData)
    {
      var result = new List<MenuModel>();


      //var dash = new MenuModel()
      //{
      //    Name = "/",
      //    Caption = "Dashboard",
      //    CSSClass = "fa-dashboard",
      //    Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "supervisor", "inspector"))

      //};


      #region Operaciones
      var oper = new MenuModel()
      {
        Name = "operaciones",
        Caption = "Operaciones",
        CSSClass = "fa-laptop",
        ItemCssOpen = "open",
        ItemCssActive = "active",
        ChildMenuList = new List<MenuModel>()
      };






      oper.ChildMenuList.Add(new MenuModel()
      {
        Name = "inspecciones",
        Caption = "Inspecciones",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "supervisor", "inspector"))
      });


      oper.ChildMenuList.Add(new MenuModel()
      {
        Name = "verificaciones",
        Caption = "Verificaciones",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector"))
      });

      oper.ChildMenuList.Add(new MenuModel()
      {
        Name = "precargas",
        Caption = "Precargas",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector"))
      });

      oper.ChildMenuList.Add(new MenuModel()
      {
        Name = "actividades",
        Caption = "Actividades",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector"))

      });


      #endregion

      #region Cliente
      var cliente = new MenuModel()
      {
        Name = "cliente",
        Caption = "Cliente",
        CSSClass = "fa-briefcase",
        ItemCssOpen = "open",
        ItemCssActive = "active",
        ChildMenuList = new List<MenuModel>()
      };

      cliente.ChildMenuList.Add(new MenuModel()
      {
        Name = "planificaciones",
        Caption = "Planificaciones",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))
      });



      cliente.ChildMenuList.Add(new MenuModel()
      {
        Name = "sellos",
        Caption = "Sellos",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))
      });

      cliente.ChildMenuList.Add(new MenuModel()
      {
        Name = "choferes",
        Caption = "Choferes",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))
      });


      cliente.ChildMenuList.Add(new MenuModel()
      {
        Name = "camiones",
        Caption = "Camiones",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))
      });


      cliente.ChildMenuList.Add(new MenuModel()
      {
        Name = "cisternas",
        Caption = "Cisternas",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))
      });
      #endregion

      #region Configuracion
      var configuracion = new MenuModel()
      {
        Name = "configuracion",
        Caption = "Configuracion",
        CSSClass = "fa-gears",
        ItemCssOpen = "open",
        ItemCssActive = "active",
        ChildMenuList = new List<MenuModel>()
      };

      configuracion.ChildMenuList.Add(new MenuModel()
      {
        Name = "empresas",
        Caption = "Empresas",
        Visible = claims.Any(s => s.Contains("sadmin"))

      });

      configuracion.ChildMenuList.Add(new MenuModel()
      {
        Name = "clientes",
        Caption = "Clientes",
        Visible = claims.Any(s => s.Contains("sadmin"))
      });

      configuracion.ChildMenuList.Add(new MenuModel()
      {
        Name = "productos",
        Caption = "Productos",
        Visible = claims.Any(s => s.Contains("sadmin"))
      });

      configuracion.ChildMenuList.Add(new MenuModel()
      {
        Name = "terminales",
        Caption = "Terminales",
        Visible = claims.Any(s => s.Contains("sadmin"))
      });
      configuracion.ChildMenuList.Add(new MenuModel()
      {
        Name = "usuarios",
        Caption = "Usuarios",
        Visible = claims.Any(s => s.Contains("sadmin"))
      });

      //configuracion.ChildMenuList.Add(new MenuModel()
      //{
      //    Name = "permisos",
      //    Caption = "Roles/Permisos",
      //    Roles = new List<string>() { "sadmin" }
      //});

      #endregion

      #region Reportes
      var reportes = new MenuModel()
      {
        Name = "reportes",
        Caption = "Reportes",
        CSSClass = "fa-file-text",
        ItemCssOpen = "open",
        ItemCssActive = "active",
        ChildMenuList = new List<MenuModel>()
      };

      reportes.ChildMenuList.Add(new MenuModel()
      {
        Name = "reporteGeneral",
        Caption = "General",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))

      });

      reportes.ChildMenuList.Add(new MenuModel()
      {
        Name = "reporteAct",
        Caption = "Actividades",
        Visible = claims.Any(s => s.ToLower().OrEquals("sadmin", "admin", "supervisor", "inspector", "representante"))
      });



      #endregion

      oper.Visible = sessionData?.ClienteId > 0
                     && oper.ChildMenuList.Any(s => s.Visible);

      cliente.Visible = sessionData?.ClienteId > 0
                        && cliente.ChildMenuList.Any(s => s.Visible);

      reportes.Visible = sessionData?.ClienteId > 0
                        && reportes.ChildMenuList.Any(s => s.Visible);

      configuracion.Visible = configuracion.ChildMenuList.Any(s => s.Visible);

  


      result.Add(oper);
      result.Add(cliente);
      result.Add(reportes);
      result.Add(configuracion);
      return result;
    }
  }



}
