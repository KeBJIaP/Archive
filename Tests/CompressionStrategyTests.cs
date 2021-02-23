using Archive.Compressing.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class CompressionStrategyTests
    {
        [TestMethod]
        public void DecompressShouldInvertCompress()
        {
            //init
            var strat = new GzipCompressionStrategy();
            //act
            var bytes = new byte[] { 1, 2, 3, 4, 5, 7, 77, 232, 43, 5, 34, 53, 45, 34, 53, 45, 34, 54, 5, 34, 21, 1, 2, 33, 6, 12, 6, 21, 3, 65, 3, 4, 2, 4 };
            var compressed = strat.Compress(bytes);
            var decompressed = strat.Decompress(compressed);

            //assert
            Assert.IsTrue(Enumerable.SequenceEqual(decompressed, bytes));
        }
    }
}
