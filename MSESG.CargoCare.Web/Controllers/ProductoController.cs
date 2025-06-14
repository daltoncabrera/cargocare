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
    public class ProductoController : BaseController
    {
        public ProductoController(UnitOfWork uow):base(uow)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _unitOfWork.ProductoRepository.All.OrderByDescending(s => s.CreatedDate);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _unitOfWork.ProductoRepository.All.FirstOrDefault(s => s.Id == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Producto producto)
        {
            if (ModelState.IsValid)
            {
                FillUpdateable(producto);
                _unitOfWork.ProductoRepository.Insert(producto);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Producto producto)
        {
            if (ModelState.IsValid)
            {

                FillUpdateable(producto);
                _unitOfWork.ProductoRepository.Update(producto);
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
            var result = _unitOfWork.ProductoRepository.All.FirstOrDefault(s => s.Id == id);
            if (result == null)
                return NotFound("entity not found");

            _unitOfWork.ProductoRepository.Delete(result);
            _unitOfWork.Save();
            return Ok();
        }

    }
}
