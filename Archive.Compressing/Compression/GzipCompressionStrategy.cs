using System.IO;
using System.IO.Compression;

namespace Archive.Compressing.Compression
{
    public class GzipCompressionStrategy : ICompressionStrategy, IDecompressionStrategy
    {
        public byte[] Compress(byte[] bytesToCompress)
        {
            //берем пачку данных, компрессим и суем в очередь на запись в выходной файл
            using (var dataToCompressStream = new MemoryStream(bytesToCompress))
            {
                var dataToWriteStream = new MemoryStream();
                //gzipStream надо закрывать первым
                using (var gzipStream = new GZipStream(dataToWriteStream, CompressionMode.Compress))
                {
                    dataToCompressStream.CopyTo(gzipStream);
                    gzipStream.Flush();
                }

                var result = dataToWriteStream.ToArray();

                dataToWriteStream.Flush();
                dataToWriteStream.Dispose();

                return result;
            }
        }

        public byte[] Decompress(byte[] data)
        {
            var ms = new MemoryStream(data);
            var outputStream = new MemoryStream();

            using (var gzipStream = new GZipStream(ms, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(outputStream);
                gzipStream.Flush();
            }

            //прочитали порцию данных
            var result = outputStream.ToArray();
            //можно и записать в выходную штуку

            ms.Dispose();
            outputStream.Dispose();

            return result;
        }
    }
}
