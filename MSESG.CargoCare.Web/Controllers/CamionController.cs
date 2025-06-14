using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
    [Route("api/[controller]")]
    public class CamionController : BaseController
    {
        public CamionController(UnitOfWork uow)

            : base(uow)
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _unitOfWork.CamionRepository.All.Where(s => s.ClienteId == CurAppSessionData.ClienteId).OrderByDescending(s => s.CreatedDate);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var camion = id > 0
                ? _unitOfWork.CamionRepository.All.First(s => s.Id == id)
                : new Camion();
            if(camion.ClienteId > 0  && camion.ClienteId != CurAppSessionData.ClienteId)
                return BadRequest("No permitido");

            var cisternas = _unitOfWork.CisternaRepository
                .GetByCliente(CurAppSessionData.ClienteId)
                .Select( s => new KeyValue(s.Id.ToString(), s.Ficha + " : " + s.Placa));


            var choferes = _unitOfWork.ChoferRepository.GetByCliente(CurAppSessionData.ClienteId)
                .Select(s => new KeyValue(s.Id.ToString(), s.Nombre + " - " + s.Cedula));

            var result = new { Camion = camion, Cisternas = cisternas, Choferes = choferes};
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Camion Camion)
        {
            if (ModelState.IsValid)
            {
                FillUpdateable(Camion);
                _unitOfWork.CamionRepository.Insert(Camion);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Camion camion)
        {
            if (ModelState.IsValid)
            {
                EnsureCliente(camion);
                FillUpdateable(camion);

                camion.EmpresaId = CurAppSessionData.EmpresaId;
                _unitOfWork.CamionRepository.Update(camion);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _unitOfWork.CamionRepository.All.FirstOrDefault(s => s.Id == id);
            if (result == null)
                return NotFound("entity not found");

            _unitOfWork.CamionRepository.Delete(result);
            _unitOfWork.Save();
            return Ok();
        }

    }

}
