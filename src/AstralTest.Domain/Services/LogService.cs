using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralTest.Domain.Interfaces;
using AstralTest.Logs;
using System.IO;

#if NET461
using PdfSharp.Pdf;
using PdfSharp.Drawing;
#endif

namespace AstralTest.Domain.Services
{
    /// <summary>
    /// Класс для добалвения логов о пользователях
    /// </summary>
    public class LogService : ILogService<LogModel>
    {
        private readonly IActionService _actionService;
        private readonly IInfoActionService _infoActionService;

        public LogService(IActionService actionService, IInfoActionService infoActionService)
        {
            _actionService = actionService;
            _infoActionService = infoActionService;
        }

     

        /// <summary>
        /// Создаёт логи на основе модели, которая включает название контроллера, действия, имя пользоватея
        /// и параметры
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        public async Task Log(LogModel logModel)
        {
            if (logModel == null)
            {
                throw new NullReferenceException("Ссылка на модель указывает на null");
            }
            var actionId = await _actionService.AddAsync(logModel.UserName, logModel.NameController,
                logModel.NameAction);

            await _infoActionService.AddAsync(logModel.Parametrs, actionId);
        }

        public async Task<byte[]> ConvertToPdf(IEnumerable<LogModel> List)
        {
#if NET461
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 14, XFontStyle.Italic);

            string result = "";
            int y = 20;
            foreach (var item in List)
            {
                result = $"{item.UserName}|{item.NameController}|{item.NameController}|{item.Parametrs}\n";
                gfx.DrawString(result, font, XBrushes.Black, 20, y);
                y += 20;
            }

            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return await Task.FromResult(stream.GetBuffer());
            }
#else
            return null;
#endif
        }
    }
}
