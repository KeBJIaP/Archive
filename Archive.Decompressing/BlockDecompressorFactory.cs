using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using Archive.Common.Containers.UnityContainers;
using System;

namespace Archive.Decompressing
{
    public class BlockDecompressorFactory : Factory, IDecompressorFactory
    {
        public BlockDecompressorFactory(
            ICompressingSettings compressionSettings,
            IAppSettings settings
            ) : base(
                RegInfo.Create<ICompressingSettings>(compressionSettings),
                RegInfo.Create<IAppSettings>(settings)
                )
        {
        }

        public IFileDecompressor Create()
        {
            using (var cont = GetContainer())
            {
                cont.Register<IDecompressedFileWriter, DecompressedFileWriter>();
                cont.Register<IFileWriteStrategy, DecompressionFileWriteStrategy>();
                cont.Register<ICompressedFileReader, CompressedFileReader>();

                return cont.Resolve<FileDecompressor>();
            }
        }
    }
}
