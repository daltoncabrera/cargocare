using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
    public class CisternaRepository : Repository<Cisterna>
    {
        private ApplicationDbContext _dbContext;
        public CisternaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Cisterna> GetByCliente(int clienteId)
        {
            return All.Where(s => s.ClienteId == clienteId).ToList();
        }


        public void SaveCisterna(Cisterna cisterna, List<CisternaDetalle> detalle)
        {
            if (cisterna.Id > 0)
            {
                Update(cisterna);
                var oldDeta = _dbContext.CisternaDetalles.Where(s => s.CisternaId == cisterna.Id);

              
                _dbContext.CisternaDetalles.RemoveRange(oldDeta);
                _dbContext.SaveChanges();
            }
            else
            {
                Insert(cisterna);
                _dbContext.SaveChanges();
            }
            foreach (var deta in detalle)
            {
                deta.CisternaId = cisterna.Id;
                deta.ClienteId = cisterna.ClienteId;
                deta.EmpresaId = cisterna.EmpresaId;
                _dbContext.CisternaDetalles.Add(deta);

            }
            _dbContext.SaveChanges();
        }

        public void DeleteCisterna(Cisterna cisterna)
        {
            var detalle = GetDetalle(cisterna.Id);
            _dbContext.CisternaDetalles.RemoveRange(detalle);
            _dbContext.Cisternas.Remove(cisterna);
            _dbContext.SaveChanges();
        }

        public IEnumerable<CisternaDetalle> GetDetalle(int cisternaId)
        {
            return _dbContext.CisternaDetalles.Where(s => s.CisternaId == cisternaId);
        }

        public IEnumerable<CisternaDetalle> GetAllDetalle(IEnumerable<int> cisternasIdList)
        {
            return _dbContext.CisternaDetalles.Where(s => cisternasIdList.Contains(s.CisternaId));
        }
    }
}
