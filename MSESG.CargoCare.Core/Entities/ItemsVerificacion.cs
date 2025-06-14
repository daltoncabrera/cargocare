using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Item
  {
    public Item(string name, string descripcion, int orden = 1)
    {
      this.Name = name;
      this.Descripcion = descripcion;
      this.Orden = orden;
    }

    public string Descripcion { get; set; }
    public string Name { get; set; }
    public int Orden { get; set; }
    public bool? Bueno { get; set; }
    public bool? Regular { get; set; }
    public bool? Malo { get; set; }
    public bool? Si { get; set; }
    public bool? No { get; set; }
  }

  public class ItemsVerificacion
  {
    public ItemsVerificacion()
    {

    }

    public ItemsVerificacion(IList<VerificacionDetalle> detalle)
    {
      fillItem(detalle);

    }

    private void fillItem(IList<VerificacionDetalle> detalle)
    {
      Camion = detalle.FirstOrDefault(s => s.ItemName == "Camion") ?? new VerificacionDetalle();
      Gomas = detalle.FirstOrDefault(s => s.ItemName == "Gomas") ?? new VerificacionDetalle();
      LineaVida = detalle.FirstOrDefault(s => s.ItemName == "LineaVida") ?? new VerificacionDetalle();
      LucesParqueo = detalle.FirstOrDefault(s => s.ItemName == "LucesParqueo") ?? new VerificacionDetalle();
      LucesFreno = detalle.FirstOrDefault(s => s.ItemName == "LucesFreno") ?? new VerificacionDetalle();
      LucesAltasBajas = detalle.FirstOrDefault(s => s.ItemName == "LucesAltasBajas") ?? new VerificacionDetalle();
      LucesMarcha = detalle.FirstOrDefault(s => s.ItemName == "LucesMarcha") ?? new VerificacionDetalle();
      EquipoCarretera = detalle.FirstOrDefault(s => s.ItemName == "EquipoCarretera") ?? new VerificacionDetalle();
      HojaMsds = detalle.FirstOrDefault(s => s.ItemName == "HojaMsds") ?? new VerificacionDetalle();
      RotuloMaterial = detalle.FirstOrDefault(s => s.ItemName == "RotuloMaterial") ?? new VerificacionDetalle();
      BandejaDrenage = detalle.FirstOrDefault(s => s.ItemName == "BandejaDrenage") ?? new VerificacionDetalle();
      ConductorUniformado = detalle.FirstOrDefault(s => s.ItemName == "ConductorUniformado") ?? new VerificacionDetalle();
      CompartimentosIdentifiacados = detalle.FirstOrDefault(s => s.ItemName == "CompartimentosIdentifiacados") ?? new VerificacionDetalle();
      CalzoSeguridad = detalle.FirstOrDefault(s => s.ItemName == "CalzoSeguridad") ?? new VerificacionDetalle();
      CompartimentosAnillos = detalle.FirstOrDefault(s => s.ItemName == "CompartimentosAnillos") ?? new VerificacionDetalle();
      CompartimentosAnillos = detalle.FirstOrDefault(s => s.ItemName == "CompartimentosAnillos") ?? new VerificacionDetalle();
      CintaReflectiva = detalle.FirstOrDefault(s => s.ItemName == "CintaReflectiva") ?? new VerificacionDetalle();
      Guantes = detalle.FirstOrDefault(s => s.ItemName == "Guantes") ?? new VerificacionDetalle();
      Casco = detalle.FirstOrDefault(s => s.ItemName == "Casco") ?? new VerificacionDetalle();
      ZapatosSeguridad = detalle.FirstOrDefault(s => s.ItemName == "ZapatosSeguridad") ?? new VerificacionDetalle();
      Arnes = detalle.FirstOrDefault(s => s.ItemName == "Arnes") ?? new VerificacionDetalle();
    }


    public VerificacionDetalle Camion = new VerificacionDetalle("Camion", "Revisión del Camión");
    public VerificacionDetalle Gomas = new VerificacionDetalle("Gomas", "Gomas");
    public VerificacionDetalle LineaVida = new VerificacionDetalle("LineaVida", "Línea de vida");
    public VerificacionDetalle LucesParqueo = new VerificacionDetalle("LucesParqueo", "Luces de Parqueo");
    public VerificacionDetalle LucesFreno = new VerificacionDetalle("LucesFreno", "Luces de frenos");
    public VerificacionDetalle LucesAltasBajas = new VerificacionDetalle("LucesAltasBajas", "Luces altas/bajas");
    public VerificacionDetalle LucesDireccionales = new VerificacionDetalle("LucesDireccionales", "Luces direccionales");
    public VerificacionDetalle LucesMarcha = new VerificacionDetalle("LucesMarcha", "Luces de Marcha atrás");
    public VerificacionDetalle LucesGalibo = new VerificacionDetalle("LucesGalibo", "Luces de Galibo");
    public VerificacionDetalle EquipoCarretera = new VerificacionDetalle("EquipoCarretera", "Equipo de carretera: Triangulo/Conos, extintor,Llanta de repuesto");
    public VerificacionDetalle HojaMsds = new VerificacionDetalle("HojaMsds", "Hoja MSDS del producto");
    public VerificacionDetalle RotuloMaterial = new VerificacionDetalle("RotuloMaterial", "Rótulo de material peligroso");
    public VerificacionDetalle BandejaDrenage = new VerificacionDetalle("BandejaDrenage", "La bandeja de drenaje  limpiada apropiadamente");
    public VerificacionDetalle ConductorUniformado = new VerificacionDetalle("ConductorUniformado", "El conductor esta Debidamente Uniformado e identificado");
    public VerificacionDetalle CompartimentosIdentifiacados = new VerificacionDetalle("CompartimentosIdentifiacados", "Los compartimientos están debidamente identificados");
    public VerificacionDetalle CalzoSeguridad = new VerificacionDetalle("CalzoSeguridad", "Al estacionarse, el chofer coloco el calzo de seguridad");
    public VerificacionDetalle CompartimentosAnillos = new VerificacionDetalle("CompartimentosAnillos", "los anillos donde colocan los sellos de seguidad de los compartimientos estan en buenas condiciones");
    public VerificacionDetalle CintaReflectiva = new VerificacionDetalle("CintaReflectiva", " El Camion Tiene cinta reflectiva de señalización");
    public VerificacionDetalle Guantes = new VerificacionDetalle("Guantes", "Guantes");
    public VerificacionDetalle Casco = new VerificacionDetalle("Casco", "Casco");
    public VerificacionDetalle ZapatosSeguridad = new VerificacionDetalle("ZapatosSeguridad", "Zapatos de Seguridad");
    public VerificacionDetalle Arnes = new VerificacionDetalle("Arnes", "Arnés");
  }

}
