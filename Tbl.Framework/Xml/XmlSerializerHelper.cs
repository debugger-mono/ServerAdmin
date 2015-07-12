using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Tbl.Framework.Xml
{
    internal static class XmlSerializerHelper
    {
        private static readonly object sync = new object();
        private static readonly Dictionary<Type, XmlSerializer> serializer;

        static XmlSerializerHelper()
        {
            serializer = new Dictionary<Type, XmlSerializer>();
        }

        /// <summary>
        /// Serializes the specified obj to xml string.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            string result = null;
            XmlSerializer serializer = GetSerializer(typeof(T));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                result = writer.ToString();
            }

            return result;
        }

        /// <summary>
        /// Serializes the specified obj to file stream. Consumer should close the file stream once done.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static FileStream SerializeToFileStream<T>(T obj, string filePath = "")
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetExecutingAssembly().GetName().Name, string.Format("{0}.xml", DateTime.Now.ToFileTime()));
            }

            FileStream fs = new FileStream(filePath, FileMode.Create);
            XmlSerializer serializer = GetSerializer(typeof(T));
            serializer.Serialize(fs, obj);
            fs.Position = 0;

            return fs;
        }

        /// <summary>
        /// Serializes the specified obj to memory stream. Consumer should close the memory stream once done.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static MemoryStream SerializeToStream<T>(T obj)
        {
            MemoryStream ms = new MemoryStream();
            XmlSerializer serializer = GetSerializer(typeof(T));
            serializer.Serialize(ms, obj);
            ms.Position = 0;

            return ms;
        }

        /// <summary>
        /// Deserializes the xml string to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="xml">The xml string</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = GetSerializer(typeof(T));
                    return (T)serializer.Deserialize(sr);
                }
            }

            return default(T);
        }

        /// <summary>
        /// Deserializes the stream contents to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="stream">The stream</param>
        /// <returns></returns>
        public static T DeserializeFromStream<T>(Stream stream)
        {
            XmlSerializer serializer = GetSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        /// Deserializes the file contents to object instance of specific type.
        /// </summary>
        /// <typeparam name="T">The type of param</typeparam>
        /// <param name="fileName">The filename</param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer serializer = GetSerializer(typeof(T));
                return (T)serializer.Deserialize(fs);
            }
        }

        private static XmlSerializer GetSerializer(Type objType)
        {
            if (!serializer.ContainsKey(objType))
            {
                lock (sync)
                {
                    if (!serializer.ContainsKey(objType))
                    {
                        serializer.Add(objType, new XmlSerializer(objType));
                    }
                }
            }

            return serializer[objType];
        }
    }
}