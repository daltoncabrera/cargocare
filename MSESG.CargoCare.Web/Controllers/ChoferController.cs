using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
    [Route("api/[controller]")]
    public class ChoferController : BaseController
    {

        public ChoferController(UnitOfWork uow) : base(uow)
        {
        }



        [HttpGet]
        public IActionResult Get()
        {
            var result = _unitOfWork.ChoferRepository.All.Where(s => s.ClienteId == CurAppSessionData.ClienteId).OrderByDescending(s => s.CreatedDate);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _unitOfWork.ChoferRepository.All.FirstOrDefault(s => s.Id == id && s.ClienteId == CurAppSessionData.ClienteId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Chofer Chofer)
        {
            if (ModelState.IsValid)
            {
                FillUpdateable(Chofer);
                _unitOfWork.ChoferRepository.Insert(Chofer);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Chofer Chofer)
        {
            if (ModelState.IsValid)
            {
                EnsureCliente(Chofer);
                FillUpdateable(Chofer);

                Chofer.EmpresaId = CurAppSessionData.EmpresaId;
                _unitOfWork.ChoferRepository.Update(Chofer);
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
            var result = _unitOfWork.ChoferRepository.All.FirstOrDefault(s => s.Id == id);
            if (result == null)
                return NotFound("entity not found");

            EnsureCliente(result);

            _unitOfWork.ChoferRepository.Delete(result);
            _unitOfWork.Save();
            return Ok();
        }

    }

}
