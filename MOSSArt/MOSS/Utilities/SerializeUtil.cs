using System;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace MOSSArt
{
    public class SerializeUtil
    {
        public static string Serialize(object obj)
        {
            string serializeStr = default(string);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(memoryStream, Encoding.UTF8);
                serializer.Serialize((XmlWriter)writer, obj);
                serializeStr = Encoding.UTF8.GetString(memoryStream.ToArray()).Trim();
            }

            return serializeStr;
        }

        public static object Deserialize(Type type, string xml)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader textReader = new StringReader(xml);
            return serializer.Deserialize(textReader);
        }
    }
}
