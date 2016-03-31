using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace percip.io
{
    class XMLDataSaver : IDataSaver
    {
        /// <summary>
        /// Decrypting an object with given Key and from xml-file in filename.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filename">Path to the xml-file.</param>
        /// <param name="encryptionKey">Key for the decryption.</param>
        /// <returns>Object of type T</returns>
        /// <exception cref="FileNotFoundException">Thrown if file from filename is non-existent.</exception>
        /// <exception cref="Exception">Thrown while decryption and file opening.</exception>
        private T DecryptAndDeserialize<T>(string filename, string encryptionKey)
        {
            var key = new DESCryptoServiceProvider();
            int length = encryptionKey.Length / 2;
            byte[] k = Encoding.ASCII.GetBytes(encryptionKey.Substring(0, length));
            byte[] iV = Encoding.ASCII.GetBytes(encryptionKey.Substring(length));
            var d = key.CreateDecryptor(k, iV);
            try
            {
                using (var fs = File.Open(filename, FileMode.Open))
                {
                    using (var cs = new CryptoStream(fs, d, CryptoStreamMode.Read))
                    {
                        return (T)(new XmlSerializer(typeof(T))).Deserialize(cs);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("{0} could not be found", filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts given object with encryption key into xml-file
        /// </summary>
        /// <typeparam name="T">Type of the Object.</typeparam>
        /// <param name="filename">Path to the xml-file.</param>
        /// <param name="obj">The object to encrypt.</param>
        /// <param name="encryptionKey">The key used for encryption.</param>
        /// <exception cref="Exception">Thrown if encryption, serialisation or saving is not working.</exception>
        private void EncryptAndSerialize<T>(string filename, T obj, string encryptionKey)
        {
            var key = new DESCryptoServiceProvider();
            int length = encryptionKey.Length / 2;
            byte[] k = Encoding.ASCII.GetBytes(encryptionKey.Substring(0, length));
            byte[] iV = Encoding.ASCII.GetBytes(encryptionKey.Substring(length));
            var e = key.CreateEncryptor(k, iV);
            try
            {
                using (var fs = File.Open(filename, FileMode.Create))
                {
                    using (var cs = new CryptoStream(fs, e, CryptoStreamMode.Write))
                    {
                        (new XmlSerializer(typeof(T))).Serialize(cs, obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Providing a user and domain specific key.
        /// </summary>
        /// <returns>String with encryption key.</returns>
        private string GetKey()
        {
            string sMyKey = Environment.UserName + "@" + Environment.UserDomainName;
            int iBitSize = 16;
            if (sMyKey.Length > iBitSize)
                sMyKey = sMyKey.Substring(0, iBitSize);
            if (sMyKey.Length < iBitSize)
                for (int i = sMyKey.Length; i < iBitSize; i++)
                    sMyKey = "~" + sMyKey;
            return sMyKey;
        }

        public T Load<T>(string filename)
        {
            return DecryptAndDeserialize<T>(filename, GetKey());
        }

        public void Save<T>(string filename, T obj)
        {
            EncryptAndSerialize<T>(filename, obj, GetKey());
        }
    }
}
