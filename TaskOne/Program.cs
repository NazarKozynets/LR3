using System;
using System.IO;
using System.Collections.Generic;

namespace TaskOne
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"D:\Code\laba3\TaskOne\text-files\";
            string noFilePath = Path.Combine(directoryPath, "no_file.txt");
            string badDataPath = Path.Combine(directoryPath, "bad_data.txt");
            string overflowPath = Path.Combine(directoryPath, "overflow.txt");

            if (File.Exists(noFilePath))
            {
                File.Delete(noFilePath);
            }
            if (File.Exists(badDataPath))
            {
                File.Delete(badDataPath);
            }                
            if (File.Exists(overflowPath))
            {
                File.Delete(overflowPath);
            }
            
            try
            {
                File.WriteAllText(noFilePath, "");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Неможливо створити файл: _no_file.txt");
            }

            try
            {
                File.WriteAllText(badDataPath, "");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Неможливо створити файл: bad_data.txt");
            }

            try
            {
                File.WriteAllText(overflowPath, "");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Неможливо створити файл: overflow.txt");
            }
            
            int totalSum = 0;
                
            for (int i = 10; i <= 29; i++)
            {
                string filePath = Path.Combine(directoryPath, $"{i}.txt");

                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    string[] nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
                    
                    int num1 = int.Parse(nonEmptyLines[0]);
                    int num2 = int.Parse(nonEmptyLines[1]);

                    int result = num1 * num2;
                    Console.WriteLine($"Файл {i}.txt: {num1} * {num2} = {result}");
                    totalSum += num1 * num2;
                }
                catch (FileNotFoundException)
                {
                    File.AppendAllText(noFilePath, i + ".txt\n");
                }
                catch (FormatException)
                {
                    File.AppendAllText(badDataPath, i + ".txt\n");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Помилка вводу-виводу: {ex.Message}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Файл містить неправильну кількість рядків");
                }
                catch (OverflowException)
                {
                    File.AppendAllText(overflowPath, i + ".txt\n");
                }
            }
            
            Console.WriteLine($"Сума всіх добутків правильних файлів: {totalSum}");

            Console.WriteLine("Файли з помилками:");
            foreach (var line in File.ReadAllLines(noFilePath))
            {
                Console.WriteLine(line);
            }
            
            Console.WriteLine("Файли з неправильною датою:");
            foreach (var line in File.ReadAllLines(badDataPath))
            {
                Console.WriteLine(line);
            }
            
            Console.WriteLine("Файли з переповненням:");
            foreach (var line in File.ReadAllLines(overflowPath))
            {
                Console.WriteLine(line);
            }
        }
    }
}