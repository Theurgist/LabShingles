using ShilglesLab.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShilglesLab
{
    class TextComparisonEngine
    {
        private ShinglesComparer ShComp;
        private SuperShinglesComparer SshComp;
        private MegaShinglesComparer MshComp;

        public TextComparisonEngine()
        { 
            this.ShComp = new ShinglesComparer(84, 10);
            this.SshComp = new SuperShinglesComparer(this.ShComp);
            this.MshComp = new MegaShinglesComparer(this.SshComp);
        }

        public void Compare(string textA, string textB)
        {
            MshComp.ProcessTexts(textA, textB);
        }

        public void PrintResults()
        {
            Console.WriteLine("");
            Console.WriteLine("Shingles engine preferences: ");
            Console.WriteLine("\tHashCount==" + ShComp.HashCount);
            Console.WriteLine("\tShingleSize==" + ShComp.ShingleSize);
            Console.WriteLine("");
            Console.WriteLine("Results:");
            Console.WriteLine("\tPercentage of matching shingles:  " + ShComp.Result*100 + "%");
            Console.WriteLine("\tAmount of matching super shingles: " + SshComp.Result);
            Console.WriteLine("\tAmount of matching mega shingles: " + MshComp.Result);
            Console.WriteLine("");
        }
    }
}
