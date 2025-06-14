using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Dto;

namespace MSESG.CargoCare.Core.EFServices
{
  public class VerificacionRepository : Repository<Verificacion>
  {
    private ApplicationDbContext _ctx;
    public VerificacionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      this._ctx = dbContext;
    }

    public void Save(VerificacionDto veriDto)
    {
      if (veriDto.Verificacion.Id > 0)
      {
        _ctx.Verificaciones.Update(veriDto.Verificacion);
        _ctx.SaveChanges();
      }
      else
      {
        _ctx.Verificaciones.Add(veriDto.Verificacion);
        _ctx.SaveChanges();
      }

      saveDetalle(veriDto);
    }

    public List<VerificacionlistDto> GetByCliente(int clienteId)
    {
      var result = from r in _ctx.Verificaciones
        join u in _ctx.Users on r.InspectorId equals u.Id into usG
        join c in _ctx.Camiones on r.CamionId equals c.Id into cG
        join cis in _ctx.Cisternas on r.CisternaId equals cis.Id into cisG
        join ch in _ctx.Choferes on r.ChoferId equals ch.Id into chG
        from camion in cG.DefaultIfEmpty()
        from cisterna in cisG.DefaultIfEmpty()
        from chofer in chG.DefaultIfEmpty()
        from usuario in usG.DefaultIfEmpty()
        select new VerificacionlistDto()
        {
          Id = r.Id,
          Fecha =  r.Fecha,
          ChoferId =  r.ChoferId,
          Chofer = chofer != null ? chofer.Nombre : "",
          CamionId =  r.CamionId,
          Camion = camion != null ? string.Format("{0} {1}", camion.Placa, camion.Ficha) : "",
          CisternaId =  r.CisternaId,
          Cisterna =  cisterna != null ? string.Format("{0} {1}", cisterna.Placa, cisterna.Ficha) : "",
          InspectorId =  r.InspectorId,
          Inspector = usuario != null ? usuario.FullName : ""
        };

      return result.ToList();
    }

    private void saveDetalle(VerificacionDto veriDto)
    {
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.Gomas);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LineaVida);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LucesParqueo);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LucesFreno);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LucesAltasBajas);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LucesDireccionales);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LucesMarcha);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.LucesGalibo);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.EquipoCarretera);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.HojaMsds);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.RotuloMaterial);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.BandejaDrenage);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.ConductorUniformado);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.CompartimentosIdentifiacados);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.CalzoSeguridad);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.CompartimentosAnillos);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.CintaReflectiva);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.Guantes);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.Casco);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.ZapatosSeguridad);
      saveItem(veriDto.Verificacion.Id, veriDto.Detalle.Arnes);
    }

    private void saveItem(int id, VerificacionDetalle item)
    {
      item.VerificacionId = id;
      if (item.Id > 0)
      {
        _ctx.VerificacionDetalles.Update(item);
      }
      else
      {
        _ctx.VerificacionDetalles.Add(item);
      }

      _ctx.SaveChanges();
    }

    public VerificacionDto GetById(int id)
    {
      var verificacion = All.FirstOrDefault(s => s.Id == id);
      if (verificacion == null)
        return null;

      var detalles = _ctx.VerificacionDetalles.Where(s => s.VerificacionId == id);

      var result = new VerificacionDto();
      result.Verificacion = verificacion;
      result.Detalle = new ItemsVerificacion(detalles.ToList());
      return result;
    }
  }
}
