using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace percip.io
{
    class XMLDataSaverUnprotected : IDataSaver
    {
        public string GetVersion<T>(string filename)
        {
            try
            {
                using (var fs = File.Open(filename, FileMode.Open))
                {
                    var xml = XmlReader.Create(fs);
                    xml.ReadToFollowing("version");
                    return xml.ReadElementContentAsString();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(new FileNotFoundException("{0} could not be found", filename));
                Environment.Exit(-1);
                return default(string);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T Load<T>(string filename)
        {
            try
            {
                using (var fs = File.Open(filename, FileMode.Open))
                {
                    return (T)(new XmlSerializer(typeof(T))).Deserialize(fs);
                }
            }
            catch (InvalidOperationException)
            {
                ConversionClass.RenewData<T>(this, filename);
                return Load<T>(filename);
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("{0} could not be found", filename);
                Environment.Exit(-1);
                return default(T);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception during run: {0}", ex.Message);
                Environment.Exit(-2);
                return default(T);
            }
        }

        public void Save<T>(string filename, T obj) 
        {
            try
            {
                using (var fs = File.Open(filename, FileMode.Create))
                {
                    (new XmlSerializer(typeof(T))).Serialize(fs, obj);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Environment.Exit(-1);
            }
        }
    }
}
