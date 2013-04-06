using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace X.Compression
{
    public static class Zip
    {
        private static int SizeOf(IEnumerable<byte> enumerable)
        {
            if (enumerable == null)
            {
                return 0;
            }

            return enumerable.Count();
        }

        /// <summary>
        /// Compress data
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] content)
        {
            var inputFileStream = new MemoryStream(content);
            var outputFileStream = new MemoryStream();

            var zipStream = new GZipStream(outputFileStream, CompressionMode.Compress);

            inputFileStream.CopyTo(zipStream);
            zipStream.Close();

            return outputFileStream.ToArray();
        }

        /// <summary>
        /// Decompress data
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] content)
        {
            var inputFileStream = new MemoryStream(content);
            var outputFileStream = new MemoryStream();

            var zipStream = new GZipStream(inputFileStream, CompressionMode.Decompress);
            zipStream.CopyTo(outputFileStream);
            outputFileStream.Close();

            return outputFileStream.ToArray();
        }

        /*
        public static bool Test()
        {
            const int SizeInMb = 10;

            var input = new byte[SizeInMb * 1024 * 1024];
            new Random().NextBytes(input);

            var output = Zip.Decompress(Zip.Compress(input));

            if (input.Length != output.Length)
                return false;

            for (int i = 0; i < input.Length; i++)
                if (input[i] != output[i])
                    return false;

            return true;
        }
        */

    }
}
