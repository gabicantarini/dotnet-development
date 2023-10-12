using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var file = @"pasta\texto.txt";

            if(!File.Exists(file)) { 

                File.CreateText(file);
            }

            var fileInfo = new FileInfo(file);

            Console.WriteLine($"Nome: {fileInfo.Name}, Tamanho: {fileInfo.Length}, Data de atualização: {fileInfo.LastWriteTime}");
        }
    }
}
