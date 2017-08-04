﻿using System.IO;
using System.Threading.Tasks;

namespace AstralTest.FileStore
{
    /// <summary>
    /// Интерфейс для работы с файлами
    /// </summary>
    public interface IFileStore
    {
        /// <summary>
        ///  Создаёт файл с указанным именем из текущего потока
        /// </summary>
        /// <param name="stream">Поток из которого создаётся файл</param>
        /// <param name="nameFile">Название файла</param>
        /// <returns></returns>
        Task Create(Stream stream, string nameFile);

        /// <summary>
        /// Загружает файл с указанным именем 
        /// </summary>
        /// <param name="nameFile">Название файла, который будет загружаться</param>
        /// <returns></returns>
        Task<byte[]> Upload(string nameFile);

        /// <summary>
        /// Копирует файл в указанный путь
        /// </summary>
        /// <param name="pathFrom">Путь к файлу который надо копировать</param>
        /// <param name="pathTo">Путь по которому копируемый файл будет перемещен</param>
        /// <returns></returns>
        Task Copy(string pathFrom,string pathTo);


        /// <summary>
        /// Записывает массив байтов в поток, и возвращает его
        /// </summary>
        /// <param name="bytes">Массив байтов, которые будут записаны в поток</param>
        /// <returns></returns>
        Task<Stream> GetStreamFromBytes(byte[] bytes);
    }
}
