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

            if (typeof(T) == typeof(TimeStampCollection))
            {
                TimeStampCollection col = obj as TimeStampCollection;

            }

            ToRepair.Save(filename, obj);
        }
    }
}
