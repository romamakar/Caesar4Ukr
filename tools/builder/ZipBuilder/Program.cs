using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace ZipBuilder
{
    internal class Program
    {
        static string localPath = ".";
        static void Main()
        {
            UpdateZip();
        }

        static void UpdateZip()
        {
            var archiveFolder = ".";

            if (File.Exists("Caesar4_Ukr.zip"))
            {
                File.Delete("Caesar4_Ukr.zip");
            }

            using (FileStream zipToCreate = new FileStream("Caesar4_Ukr.zip", FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
            {
                var dataFolder = archiveFolder + "\\Data";
                string rootFolderName = Path.GetFileName(dataFolder.TrimEnd(Path.DirectorySeparatorChar));

                // Додаємо всі файли з папки до архіву разом із папкою
                foreach (string file in Directory.GetFiles(dataFolder, "*", SearchOption.AllDirectories))
                {
                    Console.WriteLine($"Processing - {file}");
                    string relativePath = Path.GetRelativePath(dataFolder, file);
                    string entryScene = Path.Combine(rootFolderName, relativePath).Replace("\\", "/"); // Відносний шлях у архіві
                    archive.CreateEntryFromFile(file, entryScene);
                }

                string additionalFile = archiveFolder + "\\languagetext.xm";
                archive.CreateEntryFromFile(additionalFile, "languagetext.xm");

            }

            Console.WriteLine("Архів успішно створено!");
        }     
    }
}
