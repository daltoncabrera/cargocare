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
    public class TrackerController : BaseController
    {
        public TrackerController(UnitOfWork uow)
            : base(uow)
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _unitOfWork.TrackRepository.All.OrderByDescending(s => s.CreatedDate);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _unitOfWork.TrackRepository.All.FirstOrDefault(s => s.Id == id);
            return Ok(result);
        }

    }
}
