using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShilglesLab.Comparers
{
    class MegaShinglesComparer : ITextComparer
    {
        public double Result { get; private set; }
        public List<List<int>> MegaA { get; set; }
        public List<List<int>> MegaB { get; set; }

        private SuperShinglesComparer SuperShinglesEngine;
        public MegaShinglesComparer(SuperShinglesComparer shComp)
        {
            this.SuperShinglesEngine = shComp;
        }
        
        public double ProcessTexts(string textA, string textB)
        {
            SuperShinglesEngine.ProcessTexts(textA, textB);

            MegaA = GetMegaShingles(SuperShinglesEngine.SuperA);
            MegaB = GetMegaShingles(SuperShinglesEngine.SuperB);

            return CalcMegaSim();
        }

        private double CalcMegaSim()
        {
            double sim = 0;

            for (int i = 0; i < MegaA.Count; i++)
            {
                int k = ShinglesComparer.CompareHashes(MegaA[i], MegaB[i]);
                if (k == MegaA[i].Count)
                {
                    sim += 1;
                }
            }

            Result = sim;
            return sim;
        }

        private List<List<int>> GetMegaShingles(List<List<int>> super)
        {
            List<List<int>> megaShingles = new List<List<int>>();
            for (int i = 0; i < super.Count; i++)
            {
                for (int j = i + 1; j < super.Count; j++)
                {
                    megaShingles.Add(new List<int>());
                    megaShingles.Last().AddRange(super[i]);
                    megaShingles.Last().AddRange(super[j]);
                }    
            }
            return megaShingles;
        }
    }
}
