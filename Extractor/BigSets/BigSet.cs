using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Extractor
{
    public class BigSet : IBigSet
    {
        public List<ulong> data;

        public BigSet()
        {
            data = new List<ulong>();
        }

        public BigSet(List<int> trueIndexs)
        {
            var max = trueIndexs.Count>0 ? trueIndexs.Max() : 0;
            max = max == 0 ? 1 : max;
            data = new List<ulong>();
            Enumerable.Range(0, ((max - 1) / 64) + 1).ToList().ForEach(
                x =>
                {
                    data.Add(0);
                }
                );

            trueIndexs?.ForEach(
                trueIndex =>
                {
                    SetTrue(trueIndex);
                }
                );
        }

        public virtual void SetValues(List<int> trueIndexs)
        {
            var max = trueIndexs.Count > 0 ? trueIndexs.Max() : 0;
            max = max == 0 ? 1 : max;
            data = new List<ulong>();
            Enumerable.Range(0, ((max - 1) / 64) + 1).ToList().ForEach(
                x =>
                {
                    data.Add(0);
                }
            );

            trueIndexs?.ForEach(
                trueIndex =>
                {
                    SetTrue(trueIndex);
                }
            );
        }

        public virtual void SetFalse(int indexB)
        {
            int ind = (indexB-1) / 64;
            int rest = (indexB-1) % 64;
            var g = (ulong)~(1 << rest);
            data[ind] = data[ind] & g;
        }

        public virtual void SetTrue(int trueIndex)
        {
            int ind = (trueIndex - 1) / 64;
            int rest = ( (trueIndex -1) % 64 ) ;
            ulong un = 1;
            ulong g = (un << rest);
            data[ind] = data[ind] | g;
        }

        public virtual int GetNextTrue()
        {
            var i = 0;
            foreach (var ind in data)
            {
                foreach (var bitNumber in Enumerable.Range(0, 64))
                {
                    ulong b = 1;
                    if (((ind >> bitNumber) & b) == b)
                    {
                        return i*64 + bitNumber + 1;
                    }
                }

                i++;
            }
            return -1;
        }

        public virtual void Intersect(IBigSet col)
        {
            BigSet col2 = (BigSet) col;
            for (int i = 0; i < data.Count; i++)
            {
                data[i] = data[i] & col2.data[i];
            }
        }

        public override bool Equals(Object obj)
        {
            var ob =  obj as BigSet;
            if (ob == null || ob.data.Count != data.Count)
            {
                return false;
            }

            for (int i = 0; i < data.Count; i++)
            {
                if(data[i] != ob.data[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            data.ForEach(
                x =>
                {
                    foreach (var bitNumber in Enumerable.Range(0, 64))
                    {
                        if (((x >> bitNumber) & 1) == 1)
                        {
                            stringBuilder.Append("1");

                        }
                        else
                        {
                            stringBuilder.Append("0");
                        }
                    }
                }
            );
            return string.Concat(stringBuilder.ToString().Reverse());
        }

        public int GetNumberA()
        {
            return data.Count * 64;
        }
    }
}
