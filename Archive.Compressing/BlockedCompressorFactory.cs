using Archive.Application.Common;
using Archive.BlockedCompressing.Base;
using Archive.Common.Containers.UnityContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Compressing
{
    public class BlockedCompressorFactory : Factory, ICompressorFactory
    {
        public BlockedCompressorFactory(
            IAppSettings fileSettings,
            ICompressingSettings compressingSettings) : base(
                RegInfo.Create<IAppSettings>(fileSettings),
                RegInfo.Create<ICompressingSettings>(compressingSettings)
                )
        {
        }

        public IFileCompressor Create()
        {
            using (var cont = GetContainer())
            {
                cont.RegisterExternallyControlledSingletone<IOutputCompressedFileWriter, OutputCompressedFileWriter>();
                cont.Register<IFileWriteStrategy, CompressionFileWriteStrategy>();

                return cont.Resolve<BlockCompressor>();
            }
        }
    }
}
