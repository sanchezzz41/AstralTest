using System;
using System.Collections.Generic;
using System.Text;
using AstralTest.Domain.Entities;
using AstralTest.FileStore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.XSSFConverter
{
    /// <summary>
    /// Статический класс для добавления сервисов
    /// </summary>
    public static class ConvertsServices
    {
        /// <summary>
        /// Метод расширения, который добавляет сервисы конвертации к XSSF в IServiceCollection
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddConvertereServices(this IServiceCollection service)
        {
            service.AddScoped<IXssfConverter, ConverterToXssf>();

            return service;
        }
    }
}
