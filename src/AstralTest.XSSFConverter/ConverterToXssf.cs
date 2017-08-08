using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Models;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;
using File = AstralTest.Domain.Entities.File;

namespace AstralTest.XSSFConverter
{
    /// <summary>
    /// Класс конвертации данных в excel
    /// </summary>
    public class ConverterToXssf : IXssfConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> UsersConvertToXssfAsync(IEnumerable<User> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Users");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Имя");
            row.CreateCell(1).SetCellValue("Роль");
            row.CreateCell(2).SetCellValue("Email");
            row.CreateCell(3).SetCellValue("Хэш пароля");
            row.CreateCell(4).SetCellValue("Соль");
            row.CreateCell(5).SetCellValue("Id");
            rowIndex++;

            foreach (var user in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(user.UserName);
                newRow.CreateCell(1).SetCellValue(user.RoleId.ToString());
                newRow.CreateCell(2).SetCellValue(user.Email);
                newRow.CreateCell(3).SetCellValue(user.PasswordHash);
                newRow.CreateCell(4).SetCellValue(user.PasswordSalt);
                newRow.CreateCell(5).SetCellValue(user.UserId.ToString());
                rowIndex++;
            }


            for (int i = 0; i < 6; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> NotesConvertToXssfAsync(IEnumerable<Note> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Notes");
            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Id Пользователя");
            row.CreateCell(1).SetCellValue("Id заметки");
            row.CreateCell(2).SetCellValue("Текст");

            rowIndex++;

            foreach (var notes in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(notes.IdUser.ToString());
                newRow.CreateCell(1).SetCellValue(notes.NoteId.ToString());
                newRow.CreateCell(2).SetCellValue(notes.Text);

                rowIndex++;
            }

            for (int i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }
             
            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> TaskContainersConvertToXssfAsync(IEnumerable<TasksContainer> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("TasksContainer");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Название");
            row.CreateCell(1).SetCellValue("Id списка");
            row.CreateCell(2).SetCellValue("Id пользователя");

            rowIndex++;

            foreach (var tasksContainer in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(tasksContainer.Name);
                newRow.CreateCell(1).SetCellValue(tasksContainer.ListId.ToString());
                newRow.CreateCell(2).SetCellValue(tasksContainer.UserId.ToString());
                rowIndex++;
            }

            for (int i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> FilesConvertToXssfAsync(IEnumerable<File> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Users");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Название файла");
            row.CreateCell(1).SetCellValue("Тип файла");
            row.CreateCell(2).SetCellValue("Время создания");
            row.CreateCell(3).SetCellValue("Id файла");

            rowIndex++;

            foreach (var file in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(file.NameFile);
                newRow.CreateCell(1).SetCellValue(file.TypeFile);
                newRow.CreateCell(2).SetCellValue(file.CreatedTime);
                newRow.CreateCell(3).SetCellValue(file.FileId.ToString());
                rowIndex++;
            }


            for (int i = 0; i < 4; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> TasksConvertToXssfAsync(IEnumerable<UserTask> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Tasks");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Текст задачи");
            row.CreateCell(1).SetCellValue("Время когда надо закончить");
            row.CreateCell(2).SetCellValue("Время когда закончили");
            row.CreateCell(3).SetCellValue("Id задачи");
            row.CreateCell(4).SetCellValue("Id списка");
            rowIndex++;

            foreach (var task in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(task.TextTask);
                newRow.CreateCell(1).SetCellValue(task.EndTime);
                newRow.CreateCell(2).SetCellValue(task.ActualTimeEnd);
                newRow.CreateCell(3).SetCellValue(task.TaskId.ToString());
                newRow.CreateCell(4).SetCellValue(task.ListId.ToString());
                rowIndex++;
            }


            for (int i = 0; i < 5; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<byte[]> AttachmentsConvertToXssfAsync(IEnumerable<Attachment> list)
        {
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Attachments");

            var rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("Id прикрепления");
            row.CreateCell(1).SetCellValue("Id файла");
            row.CreateCell(2).SetCellValue("Id задачи");

            rowIndex++;

            foreach (var attachment in list)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(attachment.AttachmentId.ToString());
                newRow.CreateCell(1).SetCellValue(attachment.FileId.ToString());
                newRow.CreateCell(2).SetCellValue(attachment.TaskId.ToString());
                rowIndex++;
            }

            for (int i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            byte[] result;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                result = ms.ToArray();
            }
            return await Task.FromResult(result);
        }
    }
}
