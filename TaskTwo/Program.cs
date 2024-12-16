using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        string folderPath = @"D:\Code\laba3\TaskTwo\images"; 

        string[] files = Directory.GetFiles(folderPath);
        Regex regexExtForImage = new Regex(@"\.(bmp|gif|tiff?|jpe?g|png)$", RegexOptions.IgnoreCase);

        foreach (string fileName in files)
        {
            try
            {
                using Bitmap bitmap = new Bitmap(fileName);
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                string newFileName = Path.Combine(
                    Path.GetDirectoryName(fileName),
                    Path.GetFileNameWithoutExtension(fileName) + "-mirrored.gif");

                if (File.Exists(newFileName))
                {
                    File.Delete(newFileName);
                }
                
                bitmap.Save(newFileName, ImageFormat.Gif);
                Console.WriteLine($"творено новий файл {newFileName}");
            }
            catch
            {
                if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
                {
                    Console.WriteLine($"Файл {fileName} не є зображенням, хоча має відповідне розширення.");
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} пропущено, оскільки не має графічного розширення.");
                }
            }
        }

        Console.WriteLine("Обробка завершена.");
        Console.ReadLine();
    }
}