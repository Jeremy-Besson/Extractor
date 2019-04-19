using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor
{
    class SearchSpace
    {
        public ulong A;
        public ulong B;
        public List<int> YesB = new List<int>();
        Data _data;

        public SearchSpace(ulong v1, ulong v2, Data data, List<int> yesB)
        {
            this.A = v1;
            this.B = v2;
            _data = data;
            YesB = yesB;
        }

        internal int GetNextToEnumerateB()
        {
            Console.WriteLine("Enum: ");
            Console.WriteLine(PrintHelper.Print(B));
                        
            foreach (var bitNumber in Enumerable.Range(0,64))
            {
                if (((B >> bitNumber) & 1) == 1)
                {
                    Console.WriteLine(bitNumber);
                    return bitNumber;
                }
            }

            return -1;
            
        }

        internal IEnumerator<SearchSpace> SplitB(int indexB)
        {
            var g = (ulong)~(1 << indexB);

            var col = _data.getB(indexB);

            yield return new SearchSpace(A,B & g,_data, new List<int>(YesB));
            var jj = new List<int>(YesB);
            jj.Add(indexB);
            yield return new SearchSpace(A & col,B & g, _data, );
        }


        
        public void IntersectA(int minus)
        {
            
        }
    }
}
