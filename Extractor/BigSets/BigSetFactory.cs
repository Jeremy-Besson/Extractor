using System;
using System.Collections.Generic;
using System.Text;

namespace Extractor.BigSets
{
    public class BigSetFactory :IBigSetFactory
    {
        public IBigSet Create(List<int> indexes=null)
        {
            if (indexes == null)
            {
                return  new BigSet();
            }
            return  new BigSet(indexes);
        }
        
        public virtual IBigSet Clone(IBigSet bigSet)
        {
            BigSet clone = (BigSet)Create();
            ((BigSet)bigSet).data.ForEach(
                d =>
                {
                    clone.data.Add(d);
                }
            );
            return clone;
        }
        
    }
}
