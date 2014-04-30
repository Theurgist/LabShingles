using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShilglesLab.Comparers
{
    class SuperShinglesComparer : ITextComparer
    {
        public double Result { get; private set; }
        public List<List<int>> SuperA { get; private set; }
        public List<List<int>> SuperB { get; private set; }

        private ShinglesComparer ShinglesEngine;
        public SuperShinglesComparer(ShinglesComparer shComp)
        {
            this.ShinglesEngine = shComp;
        }


        public double ProcessTexts(string textA, string textB)
        {
            ShinglesEngine.ProcessTexts(textA, textB);

            SuperA = GetSuperShingles(ShinglesEngine.HashesA);
            SuperB = GetSuperShingles(ShinglesEngine.HashesB);

            return CalcSuperSim();
        }



        private double CalcSuperSim()
        {
            double sim = 0;

            for (int i = 0; i < SuperA.Count; i++)
            {
                int k = ShinglesComparer.CompareHashes(SuperA[i], SuperB[i]);
                if (k == SuperA[i].Count)
                {
                    sim += 1;
                }
            }

            Result = sim;
            return sim;
        }

        private List<List<int>> GetSuperShingles(List<int> hashes, int superCount = 6)
        {
            List<List<int>> superShingles = new List<List<int>>();
            int count = hashes.Count / superCount;
            for (int i = 0; i < superCount; i++)
            {
                superShingles.Add(new List<int>());
                for (int j = 0; j < count; j++)
                {
                    superShingles[i].Add(hashes[i + superCount * j]);

                }

                //superShingles.Add(hashes.Skip(count * i).Take(count).ToList());     
            }
            return superShingles;
        }
    }
}
