using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace percip.io
{
    class ConversionClass
    {
        public void RenewData<T>(IDataSaver ToRepair, string filename)
        {
            T obj = ToRepair.Load<T>(filename);

            ToRepair.Save(filename, obj);
        }
    }
}
