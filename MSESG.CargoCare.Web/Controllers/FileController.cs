using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core.EFServices;

namespace MSESG.CargoCare.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/File")]
    public class FileController : BaseController
    {
        public FileController(UnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        [Route("LoadFiles")]
        public async Task<IActionResult> LoadFiles(int clientId)
        {

            throw new NotImplementedException();
        }


        [HttpPost]
        [Route("RemoveFile")]
        public async Task<IActionResult> RemoveFile(int id)
        {

            //var file = _orderFormService.DeleteFile(id);
            //var filePath = Path.Combine(GetFilesDirectory(), file);
            //if (System.IO.File.Exists(filePath))
            //{
            //    System.IO.File.Delete(filePath);
            //}
            throw new NotImplementedException();
        }



        [HttpGet]
        [Route("DownloadFile")]
        public async Task<FileResult> DownloadFile(int id)
        {

            //var file = _orderFormService.GetFile(id);
            //var filePath = Path.Combine(GetFilesDirectory(), file.SystemName);
            //if (System.IO.File.Exists(filePath))
            //{
            //    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            //    return File(fileBytes, "application/x-msdownload", file.FileName);
            //}
            //else
            //{
            //    throw new FileNotFoundException();
            //}

           throw new NotImplementedException();
        }


        private string? GetFilesDirectory()
        {
            var curDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "wwwroot" + Path.DirectorySeparatorChar + "files";
            return curDirectory;
        }

        [HttpPost("{id}")]
        [Route("Upload")]
        public async Task<IActionResult> Upload(int id)
        {
            var files = Request?.Form?.Files;
            var curFile = files?.FirstOrDefault();
            if (curFile != null)
            {
                var extension = "";
                if (!string.IsNullOrEmpty(curFile.FileName))
                    extension = Path.GetExtension(curFile.FileName);
                var sysFileName = string.Format("{0}_{1}{2}", curFile.FileName, Guid.NewGuid(), extension);
                var filePath = Path.Combine(GetFilesDirectory(), sysFileName);

             

               if (System.IO.File.Exists(filePath))
                    return BadRequest("file name exist, please rename it");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await curFile.CopyToAsync(stream);
                }

              
            }

            return Ok(files?.FirstOrDefault()?.FileName);
        }


    }
}