using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    //Контроллер для работы с привязками файлов к задачам
    [Route("Attachments")]
    public class AttachmentsController : Controller
    {
        private readonly IAttachmentsService _service;
        public AttachmentsController(IAttachmentsService service)
        {
            _service = service;
        }
        //Прикрепляет файл к задаче по Id's
        [HttpPost]
        public async Task AttachFile([FromQuery]Guid idTask,[FromQuery]Guid idFile)
        {
            await _service.AttachFileToTaskAsync(idTask, idFile);
        }

        //Прикрепляет новый файл к задаче
        [HttpPost("{idTask}")]
        public async Task AttachFile(Guid idTask, IFormFile file)
        {
            await _service.AttachFileToTaskAsync(idTask, file);
        }

        //Удаляет связывание 
        [HttpDelete]
        public async Task DeleteAttachment([FromQuery]Guid idTask, [FromQuery]Guid idFile)
        {
            await _service.DeleteAtachmentAsync(idTask, idFile);
        }
    }
}
