using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using Archive.Common.Containers.UnityContainers;
using Archive.Compressing.CompressingSource;
using Archive.Compressing.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Compressing
{
    public class FileBlockedCompressorFactory : Factory, ICompressorFactory
    {
        public FileBlockedCompressorFactory(
            IAppSettings fileSettings,
            ICompressingSettings compressingSettings,
            ILogger logger
            ) : base(
                RegInfo.Create<IAppSettings>(fileSettings),
                RegInfo.Create<ICompressingSettings>(compressingSettings),
                RegInfo.Create<ILogger>(logger)
                )
        {
        }

        public IFileCompressor Create()
        {
            using (var cont = GetContainer())
            {                
                cont.Register<ICompressionStrategy, GzipCompressionStrategy>();
                cont.Register<IBlocksToCompressSource, FileBlocksToCompressSource>();
                cont.RegisterExternallyControlledSingletone<IOutputWriter, OutputCompressedFileWriter>();
                cont.Register<IFileWriteStrategy, CompressionFileWriteStrategy>();

                return cont.Resolve<BlockCompressor>();
            }
        }
    }
}
