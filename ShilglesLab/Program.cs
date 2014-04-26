using ShilglesLab.Comparers;
using System;
using System.IO;
using System.Text;

namespace ShilglesLab
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("File #1: ");
                string textA = File.ReadAllText(Console.ReadLine());
                Console.Write("File #2: ");
                string textB = File.ReadAllText(Console.ReadLine());

                var ShComp = new ShinglesComparer(84, 10);
                ShComp.ProcessTexts(textA, textB);
                Console.Write("Percentage equal: " + ShComp.Result*100.0);
            }
        }
    }
}
