﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с файлами
    /// </summary>
    //[Authorize(Roles = nameof(RolesOption.User))]
    [Route("Files")]
    public class FilesController : Controller
    {
        private readonly IFileService _service;

        public FilesController(IFileService service)
        {
            _service = service;
        }

        //Загружает файл на сервер в локальное хранилище
        [HttpPost("{idTask}")]
        public Task<Guid> UploadFile(IFormFile file, Guid idTask)
        {
            return _service.AddAsynce(file, idTask);
        }

        //Возвращает файл пользователю
        [HttpGet("{idFile}")]
        public async Task<ActionResult> GetFile(Guid idFile)
        {
            var result = await _service.GetFileAsync(idFile);

            return File(result.Bytes, result.TypeFile, result.NameFile);
        }

        //Возвращает информацию о всех файлах
        [HttpGet]
        public async Task<object> GetAllFiles()
        {
            var resultView = await _service.GetInfoAboutAllFilesAsync();
            return resultView
                .Select(x => x.FilesView());
        }

        //Удаляет файл по Id
        [HttpDelete("{idFile}")]
        public async Task DeleteFile(Guid idFile)
        {
            await _service.DeleteAsync(idFile);
        }

    }
}
