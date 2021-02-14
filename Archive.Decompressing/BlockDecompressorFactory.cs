using Archive.Common.Containers.UnityContainers;
using System;

namespace Archive.Decompressing
{
    public class BlockDecompressorFactory : Factory, IDecompressorFactory
    {
        public BlockDecompressorFactory() : base()
        {

        }

        public IFileDecompressor Create()
        {
            using (var cont = GetContainer())
            {
                return cont.Resolve<>();
            }
        }
    }
}
