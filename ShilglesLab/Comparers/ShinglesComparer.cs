using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShilglesLab;

namespace ShilglesLab.Comparers
{
    class ShinglesComparer : ITextComparer
    {
        public double Result { get; private set; }
        public List<int> HashesA { get; private set; }
        public List<int> HashesB { get; private set; }

        public int HashCount { get; private set; }
        public int ShingleSize { get; private set; }

        public ShinglesComparer(int HashCount, int ShingleSize)
        {
            this.HashCount = HashCount;
            this.ShingleSize = ShingleSize;
        }

        public double ProcessTexts(string textA, string textB)
        {
            HashesA = CalcMinHashesFromText(textA, HashCount, ShingleSize);
            HashesB = CalcMinHashesFromText(textB, HashCount, ShingleSize);
            return EstimateSimilarity(HashCount);
        }

        static public int CompareHashes(List<int> hashes1, List<int> hashes2)
        {
            int sim = 0;
            for (int i = 0; i < hashes1.Count; i++)
            {
                if (hashes1[i] == hashes2[i])
                {
                    sim += 1;
                }
            }
            return sim;

        }



        private double EstimateSimilarity(int hashCount) 
        {
            double sim = CompareHashes(HashesA, HashesB);
            sim /= (double)hashCount;
            Result = sim;
            return sim;
        }


        private List<int> CalcMinHashesFromText(string text,int hashCount = 36, int shingleSize = 10)
        {
            return GetMinHashes(CalcHashes(GetShingles(Canonisation(text),shingleSize),hashCount));
        }

        private List<string> Canonisation(string text)
        {
            var resText = new List<string>();
            foreach (var word in text.ToLower().Split(Constants.StopSymbols))
            {
                if (!Constants.StopWords.Contains(word))
                {
                    resText.Add(word);
                }
            }
            return resText;
        }

        private List<string> GetShingles(List<string> words, int count = 10)
        {
            List<string> shingles = new List<string>();

            for (int i = 0; i < words.Count - count + 1; i++) 
            {
                string shingle = "";
                foreach(var w in words.Skip(i).Take(count))
                {
                    shingle += w;
                }
                shingles.Add(shingle);
            }
            return shingles;
        }

        private List<List<int>> CalcHashes(List<string> shingles, int countHashes = 36)
        {
            List<List<int>> hashes = new List<List<int>>();
            for (int i = 0; i < countHashes; i++) 
            { 
                hashes.Add(new List<int>());
                foreach(var shingle in shingles)
                {
                    hashes[i].Add(HashFunc(shingle, i));
                }
            }
            return hashes;
        }

        private List<int> GetMinHashes(List<List<int>> hashes)
        {
            List<int> minHashes = new List<int>();
            int hashNum = 0;
            foreach (var hash in hashes) 
            {
                var min = hash.First();
                int k = 0;
                int shinMin = 0;

                foreach (var sh in hash)
                {
                    if (min > sh)
                    {
                        min = sh;
                        shinMin = k;
                    }
                    k++;
                }
                Console.WriteLine("Hash #" + hashNum + " - Shingle #" + shinMin);
                hashNum++;
                minHashes.Add(min);
            }
            return minHashes;
        }


        private int HashFunc(string shingle, int num) 
        {
            num += 84;
            return Hash(shingle, 2, Constants.SimpleNumbers[num]);
        }

        private int Hash(string shingle, int p, int mod)
        {
            int hash = (int)shingle[0];
            int m = 1;
            for (int i = 1; i < shingle.Length; i++, m*=p)
            {
                hash = (hash * p) % mod + (int)shingle[i];
            }
            return hash % mod;
        }

    }
}
