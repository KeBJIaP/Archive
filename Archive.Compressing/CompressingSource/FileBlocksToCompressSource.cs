using Archive.Application.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Compressing.CompressingSource
{
    public class FileBlocksToCompressSource : IBlocksToCompressSource
    {
        private readonly IAppSettings _fileSettings;
        private readonly ICompressingSettings _compressingSettings;
        private readonly ILogger _logger;

        public FileBlocksToCompressSource(
            IAppSettings fileSettings,
            ICompressingSettings compressingSettings,
            ILogger logger)
        {
            _fileSettings = fileSettings ?? throw new ArgumentNullException(nameof(fileSettings));
            _compressingSettings = compressingSettings ?? throw new ArgumentNullException(nameof(compressingSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<byte[]> ReadAllBlocks()
        {
            //TODO вынести чтение в зависимость
            //Открываем стрим файла и обрабатываем блоки
            using (var fileStream = File.OpenRead(_fileSettings.SourceFile))
            using (var br = new BinaryReader(fileStream))
            {
                _logger.Debug($"Длина потока исходного файла '{fileStream.Length}'");
                _logger.Debug($"Будем читать блоки по '{_compressingSettings.BytesToRead}' байт, итого '{fileStream.Length / _compressingSettings.BytesToRead}' блоков с 0");

                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    yield return br.ReadBytes(_compressingSettings.BytesToRead);
                }
            }
        }
    }
}
