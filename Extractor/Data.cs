using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor
{
    public class Data
    {
        private List<IBigSet> B;
        private int maxNumberA = 0;

        public Data(List<IBigSet> d)
        {
            B = d;
            maxNumberA = 0;
            d.ForEach(
            x => 
            {
                if (((BigSet) x).data.Count != ((BigSet)d[0]).data.Count)
                {
                    var ff = "dfg";
                }
            }
            
                );
            d.ForEach(
                x =>
                {
                    maxNumberA = Math.Max(x.GetNumberA(), maxNumberA);
                }
                );
        }

        public IBigSet getB(int index)
        {
            return B[index-1];
        }

        public int NumberA()
        {
            return maxNumberA;
        }
        public int NumberB()
        {
            return B.Count;
        }

        public static Data GenerateRandomData(int numberB, int numberA, double density)
        {
            List<IBigSet> data = new List<IBigSet>();
            Enumerable.Range(1, numberB).ToList().ForEach(
                x =>
                {
                    var record = Enumerable.Range(1, numberA).OrderBy(y => Guid.NewGuid()).Take((int) (numberA *density)).ToList();
                    data.Add(
                        new BigSet(record)
                    );
                }
            );
            return  new Data(data);
        }
    }
}
