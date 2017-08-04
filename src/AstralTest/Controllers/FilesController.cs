using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //[Authorize(Roles = nameof(RolesOption.User))]
    [Route("Files")]
    public class FilesController : Controller
    {
        private readonly IFileService _service;

        public FilesController(IFileService service)
        {
            _service = service;
        }
        // GET: /<controller>/
        [HttpPost("{idTask}")]
        public Task<Guid> Index(IFormFile file,Guid idTask)
        { 
            return _service.AddAsynce(file, idTask);
        }

        [HttpGet("{idFile}")]
        public async Task<ActionResult> UploadFile(Guid idFile)
        {
            var result = await _service.GetFile(idFile);

            return File(result.Bytes,result.TypeFile,result.NameFile);
        }
      
    }
}
