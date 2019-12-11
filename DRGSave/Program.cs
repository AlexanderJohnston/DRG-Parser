using GvasFormat;
using GvasFormat.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DRGSave
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Deep Rock Galactic Save Parser: FOR KARL!");
            var saves = GetSaves();
            var formatted = FormatSaves(saves);
            Console.WriteLine(formatted);
            foreach (var file in saves)
            {
                if (file.Name == "76561197968149582_Player.sav")
                {
                    ReadSave(file);
                    Console.ReadLine();
                }
            }
                Console.ReadLine();
        }

        static List<FileInfo> GetSaves()
        {
            var path = @"B:\steam\steamapps\common\Deep Rock Galactic\FSD\Saved\SaveGames";
            return Directory.GetFiles(path).Select(file => new FileInfo(file)).ToList();
        }

        static string FormatSaves(List<FileInfo> files)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Located the following files:");
            foreach (var file in files)
            {
                sb.AppendLine($"FileName: {file.Name}");
            }
            return sb.ToString();
        }

        static void ReadSave(FileInfo file)
        {
            var bytes = ReadBytes(file.FullName);
            var parsedFile = new SaveFile(file, bytes);
            Console.WriteLine($"{bytes.Length} bytes in {file.Name}: {bytes[0]}, {bytes[1]}, {bytes[2]}, {bytes[3]}");
            Gvas save;
            using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                save = UESerializer.Read(stream);
            var json = JsonConvert.SerializeObject(save, new JsonSerializerSettings { Formatting = Formatting.Indented });
            using (var stream = File.Open(file.FullName + ".json", FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                writer.Write(json);
            //Console.WriteLine(FormatSave(parsedFile.Text));
        }

        static byte[] ReadBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        static string FormatSave(string saveFileText)
        {
            saveFileText.Replace("FloatProperty", $"FloatProperty{Environment.NewLine}");
            return saveFileText;
        }

    }
}
