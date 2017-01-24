using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libaudiomagic
{
    public class Magic
    {
        /// <summary>
        /// Determines mime type from file at path.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Mime type</returns>
        public static string DetermineMimeType(string path)
        {
            var buffer = new byte[16]; // size of largest magic #
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, buffer.Length);
            }
            var b = buffer;
            if (b.Take(4).ToArray().SequenceEqual(new byte[]{0x52,0x49, 0x46,0x46}) && b.Skip(8).Take(8).SequenceEqual(new byte[] {0x57,0x41,0x56,0x45,0x66,0x6D,0x74,0x20})) // wav
                return "audio/wav";
            if (b.Take(14).ToArray().SequenceEqual(new byte[] {0x4F,0x67,0x67,0x53, 0x00,0x02,0x00,0x00, 0x00,0x00,0x00,0x00, 0x00,0x00})) // oga/ogg
                return "audio/ogg";
            if (b.Take(12).ToArray().SequenceEqual(new byte[] {0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20}))
                return "audio/mp4";
            if (b.Take(8).ToArray().SequenceEqual(new byte[] {0x66, 0x6c, 0x61, 0x43, 0x00, 0x00, 0x00, 0x22}))
                return "audio/flac";
            if (b.Take(5).ToArray().SequenceEqual(new byte[] {0x46, 0x4F, 0x52, 0x4D, 0x00}))
                return "audio/aiff";
            if (b.Take(3).ToArray().SequenceEqual(new byte[] {0x00, 0x00, 0x01}) && 0xB0 <= b[4] && b[4] <= 0xBF)
                return "audio/mpeg";
            if (b.Take(3).ToArray().SequenceEqual(new byte[] {0x49, 0x44, 0x33}))
                return "audio/mp3";
            if (b.Take(2).ToArray().SequenceEqual(new byte[] {0xFF, 0xF1}) || b.Take(2).ToArray().SequenceEqual(new byte[] {0xFF, 0xF9}))
                return "audio/aac";

            return null;
        }

        // wav: needed 16b
        // 52 49 46 46 xx xx xx xx
        // 57 41 56 45 66 6D 74 20

        // oga, ogg: needed: 14b audio/ogg
        // 4F 67 67 53  00 02 00 00
        // 00 00 00 00  00 00

        // M4A: needed 
        // 00 00 00 20  66 74 79 70
        // 4D 34 41 20

        // flac: audio/flac
        // 66 4C 61 43  00 00 00 22

        // aiff, dax
        // 46 4F 52 4D 00

        // mpeg, mpg, mp3
        // 00 00 01 B(0-F)

        // mp3: needed 7b
        // 49 44 33

        // AAC: needed 2b
        // FF F1

        // AAC: needed 2b
        // FF F9
    }
}
