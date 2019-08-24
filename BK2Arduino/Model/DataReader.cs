using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{ 
    class DataReader
    {
        public short[] ReadDataFromFile(string filePath)
        {
            string[] text = null;

            using (ZipArchive archive = ZipFile.OpenRead(filePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith("Input Log.txt", StringComparison.OrdinalIgnoreCase))
                    {
                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(filePath, entry.FullName));

                        // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                        // are case-insensitive.
                        if (destinationPath.StartsWith(filePath, StringComparison.Ordinal))
                        {
                            if(!Directory.Exists(@"C:\\Temp"))
                                Directory.CreateDirectory(@"C:\\Temp");
                            entry.ExtractToFile(@"C:\\Temp\\" + entry.FullName);
                            text = File.ReadAllLines(@"C:\\Temp\\" + entry.Name);
                            File.Delete(@"C:\\Temp\\" + entry.FullName);
                        }
                    }
                }
            }
            return stringToShortArray(text);
        }

        private short[] stringToShortArray(string[] text)
        {
            List<short> dataList = new List<short>();
            for (int i = 2; i < text.Length; i++)
            {
                if (16 <= text[i].Length)
                {
                    string temp = Regex.Replace((text[i].Substring(4, 12)).Replace('.', '0'), "[a-zA-Z]", "1") + "0000";
                    dataList.Add(Convert.ToInt16(temp, 2));
                }
            }

            return dataList.ToArray();
        }
    }
}