using System.IO;

namespace Tbl.Framework.Xml
{
    public interface IXmlSerializerService
    {
        /// <summary>
        /// Serializes the specified obj to xml string.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        string Serialize<T>(T obj);

        /// <summary>
        /// Serializes the specified obj to file stream. Consumer should close the file stream once done.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        FileStream SerializeToFileStream<T>(T obj, string filePath = "");

        /// <summary>
        /// Serializes the specified obj to memory stream. Consumer should close the memory stream once done.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        MemoryStream SerializeToStream<T>(T obj);

        /// <summary>
        /// Deserializes the xml string to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="xml">The xml string</param>
        /// <returns></returns>
        T Deserialize<T>(string xml);

        /// <summary>
        /// Deserializes the stream contents to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="stream">The stream</param>
        /// <returns></returns>
        T DeserializeFromStream<T>(Stream stream);

        /// <summary>
        /// Deserializes the file contents to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="fileName">The filename</param>
        /// <returns></returns>
        T DeserializeFromFile<T>(string fileName);
    }
}
