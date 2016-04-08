using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace percip.io
{
    class XMLDataConverter : IDataSaver
    {
        XMLDataSaverUnprotected Unprotected = new XMLDataSaverUnprotected();
        XMLDataSaver Saver = new XMLDataSaver();
        public T Load<T>(string filename)
        {
            return Unprotected.Load<T>(filename);
        }

        public void Save<T>(string filename, T obj)
        {
            Unprotected.Save(filename, obj);
        }

        public bool Convert<T>(string filename, Source source)
        {
            T temp;
            switch (source)
            {
                case Source.FromUnprotected:
                    temp = Unprotected.Load<T>(filename);
                    Saver.Save(filename, temp);
                    break;
                case Source.FromProtected:
                    temp = Saver.Load<T>(filename);
                    Unprotected.Save(filename, temp);
                    break;
                default:
                    return false;
            }
            return true;
        }

        public string GetVersion<T>(string filename)
        {
            return ((IDataSaver)Unprotected).GetVersion<T>(filename);
        }
    }

    public enum Source
    {
        FromUnprotected,
        FromProtected
    }
}
