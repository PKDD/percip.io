using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace percip.io
{
    class ConversionClass
    {
        public static void RenewData<T>(IDataSaver ToRepair, string filename)
        {

            if (typeof(T) == typeof(TimeStampCollection))
            {
                try
                {
                    TimeStampCollection col = ToRepair.Load<TimeStampCollection>(filename);
                }
                catch (Exception)
                {
                    try
                    {
                        ToRepair.GetVersion<TimeStampCollection>(filename);
                    }
                    catch
                    {
                        try
                        {
                            percip.io_1_0_4.TimeStampCollection col_1_0_4 = ToRepair.Load<percip.io_1_0_4.TimeStampCollection>(filename);
                            TimeStampCollection col = TimeStampCollection.Convert(col_1_0_4);
                            ToRepair.Save(filename, col);
                        }
                        catch
                        {
                            Console.WriteLine("Failed to Convert Data!");
                            Environment.Exit(-2);
                        }
                    }
                }
            }
        }
    }
}
