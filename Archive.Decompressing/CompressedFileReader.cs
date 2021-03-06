﻿using Archive.Application.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace Archive.Decompressing
{
    /// <summary>
    /// Читает файл архива поблочно
    /// </summary>
    public class CompressedFileReader : ICompressedFileReader
    {
        private readonly FileStream _fileStream;
        private readonly IAppSettings _settings;

        public CompressedFileReader(IAppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _fileStream = File.OpenRead(_settings.SourceFile);
        }

        public void Dispose()
        {
            _fileStream?.Dispose();
        }

        public IEnumerable<byte[]> ReadByteBlocks()
        {
            using (var br = new BinaryReader(_fileStream))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    var blockSize = br.ReadInt32();
                    yield return br.ReadBytes(blockSize);
                }
            }
        }
    }
}
