using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace percip.io
{
    public interface IDataSaver
    {
        /// <summary>
        /// Saving an object.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filename">Property for objects location.</param>
        /// <param name="obj">The object.</param>
        void Save<T>(string filename, T obj);
        /// <summary>
        /// Loading an object.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filename">Location of the object.</param>
        /// <returns>An object of given type.</returns>
        T Load<T>(string filename);
    }
}
