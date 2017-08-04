using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AstralTest.FileStore
{
    /// <summary>
    /// Статический класс для добавленя сервисов
    /// </summary>
    public  static class FileStoreServices
    {
        /// <summary>
        /// Метод расширения, который добавляет сервисы в IServiceCollection
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configure">Конфигурации для FileStore</param>
        /// <returns></returns>
        public static IServiceCollection AddFileStoreServices(this IServiceCollection service,
            Action<FileStoreOptions> configure)
        {
            //Не работает так, последняя строчка не может принять параматр,
            //принимает только тип
            //var fileStoreOptions = new FileStoreOptions();
            //configure(fileStoreOptions);
            //service.AddScoped(fileStoreOptions);

            service.AddScoped<IFileStore, FileStore>();
            service.Configure(configure);
            service.AddScoped(x => x.GetService<IOptionsSnapshot<FileStoreOptions>>().Value);
            return service;
        }
    }
}
