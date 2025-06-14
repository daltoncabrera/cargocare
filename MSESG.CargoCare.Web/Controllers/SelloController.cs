using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Web.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
    [Route("api/[controller]")]
    public class SelloController : BaseController
    {

        public SelloController(UnitOfWork uow) : base(uow)
        {
        }



        [HttpGet]
        public IActionResult Get(int page = 1, int pageSize = 30, int totalItems = 0, int column = 0, string filter = "", int sortColumn = 0, SortDir dir = SortDir.Ascending, SelloStatus estatus = SelloStatus.None)
        {

            if (CurAppSessionData == null)
                return Unauthorized();

            IEnumerable<object> lotes = null;
            IEnumerable<object> estatusList = null;
            var objFilter = new FilterModel(page, pageSize, totalItems, column, filter, sortColumn, dir, estatus);

            var result = _unitOfWork.SelloRepository
                         .GetSellos(CurAppSessionData.ClienteId, objFilter)
                         .Select(s => new SellosDto
                {
                   Id = s.Id,
                   CodSello = s.CodSello,
                   Lote = s.Lote,
                   SelloStatusId = ((int)s.SelloStatus).ToString(),
                   SelloStatus =  CoreUtils.GetEnumDescription<SelloStatus>(s.SelloStatus),
                   CreatedDate = s.CreatedDate,

                         });

            if (objFilter.Page == 1)
            {
                lotes = _unitOfWork.SelloRepository.GetLotes(CurAppSessionData.ClienteId);
                estatusList = CoreUtils.GetEnumKeyValueList<SelloStatus>();
            }
            return Ok(new { Sellos = result, Filter = objFilter, Lotes = lotes, Estatus = estatusList });
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(int ordenId, string filter = "")
        {
            var r = _unitOfWork.SelloRepository.GetByClienteSinUsar(CurAppSessionData.ClienteId, ordenId, filter).OrderBy(s => s.Lote)
            .ThenBy(s => s.IntSello)
            .Take(30);

            return Ok(r);
        }


        [HttpGet]
        [Route("GetSellosAfter")]
        public IActionResult GetSellosAfter(int intSello)
        {
            var result = _unitOfWork.SelloRepository.All.Where(s => s.ClienteId == CurAppSessionData.ClienteId
                        && s.SelloStatus == SelloStatus.Disponible
                        && s.IntSello > intSello)
                            .OrderBy(s => s.Lote)
                            .ThenBy(s => s.IntSello)
                        .Take(30);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCreate")]
        public IActionResult GetCreate()
        {
            var lastLote = _unitOfWork.SelloRepository.All.Where(s => s.ClienteId == CurAppSessionData.ClienteId)
                         .Select(s => s.Lote).Distinct().Count();

            var inicial = _unitOfWork.SelloRepository.All.Where(s => s.ClienteId == CurAppSessionData.ClienteId)
                .OrderBy(s => s.IntSello).LastOrDefault();

            var result = new SellosLoteDto
            {
                Lote = lastLote + 1,
                Inicial = inicial != null ? inicial.IntSello + 1 : 0,
                Final = 0,
                Espacios = 6
            };

            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _unitOfWork.SelloRepository.All.FirstOrDefault(s => s.Id == id && s.ClienteId == CurAppSessionData.ClienteId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]SellosLoteDto sello)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.SelloRepository.GenerateSellos(CurAppSessionData.EmpresaId,
                    CurAppSessionData.ClienteId,
                    sello.Lote,
                    sello.Inicial,
                    sello.Final,
                    sello.Espacios,
                    CurAppSessionData.UserId,
                    CurAppSessionData.ClienteId,
                    CurAppSessionData.EmpresaId);

                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SellosDto model)
        {
            if (ModelState.IsValid)
            {
                var sello = _unitOfWork.SelloRepository.GetSelloById(model.Id);
                EnsureCliente(sello);
                FillUpdateable(sello);
                sello.SelloStatus = (SelloStatus)Convert.ToInt32(model.SelloStatusId);
                _unitOfWork.SelloRepository.Update(sello);
                _unitOfWork.Save();

                var result = new SellosDto
                {
                    Id = sello.Id,
                    CodSello = sello.CodSello,
                    Lote = sello.Lote,
                    SelloStatusId = ((int)sello.SelloStatus).ToString(),
                    SelloStatus = CoreUtils.GetEnumDescription<SelloStatus>(sello.SelloStatus),
                    CreatedDate = sello.CreatedDate,

                };

                return Ok(result);
            }
            else
            {
                return BadRequest("invalid model");
            }
        }

        [HttpDelete("{id}")]
        [Route("DeleteLote")]
        public IActionResult DeleteLote(int id)
        {
            _unitOfWork.SelloRepository.DeleteLote(id, CurAppSessionData.ClienteId);

            return Ok();
        }

    }

}

