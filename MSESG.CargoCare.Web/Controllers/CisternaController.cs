using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.EFServices.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
    [Route("api/[controller]")]
    public class CisternaController : BaseController
    {

        public CisternaController(UnitOfWork uow) : base(uow)
        {
        }

       

        [HttpGet]
        public IActionResult Get()
        {
            var result = _unitOfWork.CisternaRepository.All.Where(s => s.ClienteId == CurAppSessionData.ClienteId).OrderByDescending(s => s.CreatedDate);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cisterna = _unitOfWork.CisternaRepository.All.FirstOrDefault(s => s.Id == id && s.ClienteId == CurAppSessionData.ClienteId);
            var detalle = (cisterna != null) ? _unitOfWork.CisternaRepository.GetDetalle(cisterna.Id) : null;
            return Ok(new { Cisterna = cisterna, Detalle = detalle});
        }

        [HttpPost]
        public IActionResult Post([FromBody]CisternaDto model)
        {
            if (ModelState.IsValid)
            {
                FillUpdateable(model.Cisterna);
                _unitOfWork.CisternaRepository.SaveCisterna(model.Cisterna, model.Detalle);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CisternaDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Cisterna.ClienteId != CurAppSessionData.ClienteId)
                    return BadRequest("Invalid Model");

                FillUpdateable(model.Cisterna);
                _unitOfWork.CisternaRepository.SaveCisterna(model.Cisterna, model.Detalle);
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
            var result = _unitOfWork.CisternaRepository.All.FirstOrDefault(s => s.Id == id);
            if (result == null)
                return NotFound("entity not found");

            if (result.ClienteId != CurAppSessionData.ClienteId)
                return BadRequest("Invalid Model");

            _unitOfWork.CisternaRepository.DeleteCisterna(result);
            _unitOfWork.Save();
            return Ok();
        }

    }

}
