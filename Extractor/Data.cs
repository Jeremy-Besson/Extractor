using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extractor.BigSets;

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
                    maxNumberA = Math.Max(x.GetMaxIndexA(), maxNumberA);
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

        public static Data GenerateRandomData(IBigSetFactory bigSetFactory, int numberA, int numberB, double density)
        {
            var record = Enumerable.Range(1, numberA).OrderBy(y => Guid.NewGuid()).Take((int)(numberA * density)).ToList();
            List<IBigSet> data = new List<IBigSet>();
            Enumerable.Range(1, numberB).ToList().ForEach(
                x =>
                {
                    var record2 = new List<int>(record);

                    record2.OrderBy(y => Guid.NewGuid()).Take(20).ToList().ForEach(
                        y =>
                        {
                            record2.Remove(y);
                        }
                        );

                    record2.AddRange(
                        Enumerable.Range(1, numberA).OrderBy(y => Guid.NewGuid()).Take((int)(20)).ToList()
                        );

                    //var record = Enumerable.Range(1, numberA).OrderBy(y => Guid.NewGuid()).Take((int) (numberA *density)).ToList();
                    data.Add(
                        bigSetFactory.Create(record2)
                    );
                }
            );
            return  new Data(data);
        }
    }
}
