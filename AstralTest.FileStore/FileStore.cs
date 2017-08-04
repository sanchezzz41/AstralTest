using System;
using System.IO;
using System.Threading.Tasks;

namespace AstralTest.FileStore
{
    /// <summary>
    /// Класс для работы с файлами. Все файлы храняться в одном месте.
    /// </summary>
    public class FileStore : IFileStore
    {
        private readonly FileStoreOptions _fileStoreOption;

        /// <summary>
        /// Иницилизирует новый экземпляр класс
        /// </summary>
        /// <param name="opt">Опции, которые будут использоваться при работе класса</param>
        public FileStore(FileStoreOptions opt)
        {
            _fileStoreOption = opt;
            if (!File.Exists(opt.LocalRepository))
            {
                Directory.CreateDirectory(opt.LocalRepository);
            }
        }

        /// <summary>
        /// Создаёт файл с указанным именем из текущего потока
        /// </summary>
        /// <param name="stream">Поток из которого создаётся файл</param>
        /// <param name="nameFile">Название файла</param>
        /// <returns></returns>
        public async Task Create(Stream stream, string nameFile)
        {
            if (stream.Length == 0)
            {
                throw new Exception("В потоке нету данных.");
            }

            stream.Seek(0, SeekOrigin.Begin);

            var resultMass = new byte[stream.Length];

            stream.Read(resultMass, 0, resultMass.Length);

            using (var fileStream = new FileStream(_fileStoreOption.LocalRepository + "/" + nameFile, FileMode.Create,
                FileAccess.Write))
            {
                await fileStream.WriteAsync(resultMass, 0, resultMass.Length);
            }
        }

        /// <summary>
        /// Загружает файл с указанным именем 
        /// </summary>
        /// <param name="nameFile">Название файла, который будет загружаться</param>
        /// <returns></returns>
        public async Task<byte[]> Upload(string nameFile)
        {
            string path = _fileStoreOption.LocalRepository + "/" + nameFile;
            if (!File.Exists(path))
            {
                throw new Exception($"Такого файла {nameFile}  не существует!");
            }
            var result = File.ReadAllBytes(path);
            return result;
        }

        /// <summary>
        /// Копирует файл в указанный путь
        /// </summary>
        /// <param name="pathFrom">Путь к файлу который надо копировать</param>
        /// <param name="pathTo">Путь по которому копируемый файл будет перемещен</param>
        /// <returns></returns>
        public async Task Copy(string pathFrom, string pathTo)
        {

        }

        /// <summary>
        /// Записывает массив байтов в поток, и возвращает его
        /// </summary>
        /// <param name="bytes">Массив байтов, которые будут записаны в поток</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamFromBytes(byte[] bytes)
        {
            if (bytes.Length == 0)
            {
                return null;
            }
            var resultStream = new MemoryStream(bytes);
            return resultStream;
        }

        /// <summary>
        /// Удаляет файл по имени
        /// </summary>
        /// <param name="nameFile">Имя файла</param>
        /// <returns></returns>
        public async Task Delete(string nameFile)
        {
            string path = _fileStoreOption.LocalRepository + "/" + nameFile;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
