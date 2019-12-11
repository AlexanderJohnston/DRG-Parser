using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DRGSave
{
    public class SaveFile
    {
        public SaveFile(FileInfo file, byte[] bytes)
        {
            File = file;
            Bytes = bytes;
            Text = Encoding.ASCII.GetString(bytes);
        }

        public FileInfo File { get; set; }
        public byte[] Bytes { get; set; }
        public string Text { get; set; }
    }
}
