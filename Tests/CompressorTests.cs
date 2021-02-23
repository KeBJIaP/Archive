using Archive.Application.Common;
using Archive.Compressing;
using Archive.Compressing.CompressingSource;
using Archive.Compressing.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tests.Compressors;
using Unity;

namespace Tests
{
    [TestClass]
    public class CompressorTests
    {
        [TestMethod]
        public void CompressorShouldWriteAsManyBlocksAsItRead()
        {
            /*компрессор должен записать столько же блоков сколько прочитал из источника
            */

            //init
            var byteBlocks = new byte[][]
            {
                new byte[]{ 1,2,3,54},
                new byte[]{ 2,5,8,24}
            };

            IFileCompressor compressor = null;
            //будем считать сколько блоков прошло через особую реализацию писателя
            var writer = new WaitWriterWithCounting();
            var blocksSource = new HardcodedBlocksSource(byteBlocks);
            var compressionStrategy = new NoCompressionStrategy();
            using (var cont = new UnityContainer())
            {
                cont.RegisterInstance<ICompressingSettings>(new SimpleCompressingSettings() { MaximumThreadsToUse = 5 });
                cont.RegisterInstance<IOutputWriter>(writer);
                cont.RegisterInstance<ILogger>(new Mock<ILogger>().Object);
                cont.RegisterInstance<IBlocksToCompressSource>(blocksSource);
                cont.RegisterInstance<ICompressionStrategy>(compressionStrategy);

                compressor = cont.Resolve<BlockCompressor>();
            }

            //act
            var res = compressor.Compress();

            //assert
            Assert.AreEqual(true, res);
            Assert.AreEqual(2, writer.BlocksReceived);
        }
    }
}
