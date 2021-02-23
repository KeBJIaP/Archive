using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using Archive.Common.Containers.UnityContainers;
using Archive.Compressing.Compression;
using System;

namespace Archive.Decompressing
{
    public class BlockDecompressorFactory : Factory, IDecompressorFactory
    {
        public BlockDecompressorFactory(
            ICompressingSettings compressionSettings,
            IAppSettings settings,
            ILogger logger
            ) : base(
                RegInfo.Create<ICompressingSettings>(compressionSettings),
                RegInfo.Create<IAppSettings>(settings),
                RegInfo.Create<ILogger>(logger)
                )
        {
        }

        public IFileDecompressor Create()
        {
            using (var cont = GetContainer())
            {                
                cont.Register<IDecompressionStrategy, GzipCompressionStrategy>();
                cont.Register<IDecompressedFileWriter, DecompressedFileWriter>();
                cont.Register<IFileWriteStrategy, DecompressionFileWriteStrategy>();
                cont.Register<ICompressedFileReader, CompressedFileReader>();

                return cont.Resolve<FileDecompressor>();
            }
        }
    }
}
