using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Hello World!");

            // Пока что нужно устанавливать абсолютный путь к файлу - потому что проект может лежать на флешке
            // Также на данный момент есть ограничение:
            // Парсер пока не может нормально обработать путь с пробелами, например [c:/user/новая папка/project alpha.aep]
            // Причину можно узнать в проекте go-test в файле main.go
            string filepath = @$"{Directory.GetCurrentDirectory()}\go-test\data\Layer-01.aep";
            runCommand(filepath);
        }

        static void runCommand(string aepPath)
        {
            Process process = new Process();
            // запускаем сборку на golang
            process.StartInfo.FileName = @"go-test\main.exe";
            // с аргументами нужно быть поаккуратнее (путь не должен содержать пробелы)
            process.StartInfo.Arguments = aepPath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            
            process.OutputDataReceived += OutputHandler;
            process.ErrorDataReceived += OutputHandler;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (outLine.Data is null)
                return;

            if (outLine.Data.StartsWith("panic"))
                throw new Exception($"parser error: {outLine.Data}");

            // Read more: https://www.newtonsoft.com/json/help/html/DeserializeObject.htm
            //Project project = JsonConvert.DeserializeObject<Project>(outLine.Data);

            // Весь JSON результат выводится в одну строчку. Форматируем, чтобы было проще читать.
            JToken jt = JToken.Parse(outLine.Data);
            string formatted = jt.ToString(Newtonsoft.Json.Formatting.Indented);
            
            File.WriteAllText(@"result.txt", formatted);
            Console.WriteLine(formatted);
        }
    }
}
