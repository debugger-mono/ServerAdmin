using System.IO;

namespace Tbl.Framework.Xml
{
    public sealed class XmlSerializerService : IXmlSerializerService
    {
        /// <summary>
        /// Serializes the specified obj to xml string.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public string Serialize<T>(T obj)
        {
            return XmlSerializerHelper.Serialize<T>(obj);
        }

        /// <summary>
        /// Serializes the specified obj to file stream. Consumer should close the file stream once done.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public FileStream SerializeToFileStream<T>(T obj, string filePath = "")
        {
            return XmlSerializerHelper.SerializeToFileStream<T>(obj, filePath);
        }

        /// <summary>
        /// Serializes the specified obj to memory stream. Consumer should close the memory stream once done.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public MemoryStream SerializeToStream<T>(T obj)
        {
            return XmlSerializerHelper.SerializeToStream<T>(obj);
        }

        /// <summary>
        /// Deserializes the xml string to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="xml">The xml string</param>
        /// <returns></returns>
        public T Deserialize<T>(string xml)
        {
            return XmlSerializerHelper.Deserialize<T>(xml);
        }

        /// <summary>
        /// Deserializes the stream contents to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="stream">The stream</param>
        /// <returns></returns>
        public T DeserializeFromStream<T>(Stream stream)
        {
            return XmlSerializerHelper.DeserializeFromStream<T>(stream);
        }

        /// <summary>
        /// Deserializes the file contents to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="fileName">The filename</param>
        /// <returns></returns>
        public T DeserializeFromFile<T>(string fileName)
        {
            return XmlSerializerHelper.DeserializeFromFile<T>(fileName);
        }
    }
}
