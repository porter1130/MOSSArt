using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace MOSSArt
{
    [Serializable, XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        private Dictionary<string, XmlSerializer> XmlSerializer;

        public SerializableDictionary()
        {
            this.XmlSerializer = new Dictionary<string, XmlSerializer>();
        }

        //public SerializableDictionary(SerializationInfo info, StreamingContext context)
        //{
        //    int itemCount = info.GetInt32("ItemCount");
        //    for (int i = 0; i < itemCount; i++)
        //    {
        //        KeyValuePair<TKey, TValue> kvp = (KeyValuePair<TKey, TValue>)info.GetValue(string.Format("Item{0}", i), typeof(KeyValuePair<TKey, TValue>));
        //        this.Add(kvp.Key, kvp.Value);
        //    }
        //}

        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("ItemCount", this.Count);
        //    int itemIdx = 0;
        //    foreach (KeyValuePair<TKey, TValue> kvp in this)
        //    {
        //        info.AddValue(string.Format("Item{0}", itemIdx), kvp, typeof(KeyValuePair<TKey, TValue>));
        //        itemIdx++;
        //    }
        //    base.GetObjectData(info, context);
        //}

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TKey));
            XmlSerializer xmlSerializer = null;
            bool isEmptyElement = reader.IsEmptyElement;
            reader.Read();
            if (!isEmptyElement)
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    string attribute = reader.GetAttribute("type");
                    reader.ReadStartElement("item");
                    xmlSerializer = this.GetXmlSerializer(attribute);
                    reader.ReadStartElement("key");
                    TKey key = (TKey)serializer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("value");
                    TValue value = (TValue)xmlSerializer.Deserialize(reader);
                    reader.ReadEndElement();
                    base.Add(key, value);
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TKey));

            foreach (TKey key in base.Keys)
            {
                writer.WriteStartElement("item");
                TValue value = base[key];
                string assemblyQualifiedName = value.GetType().AssemblyQualifiedName;
                writer.WriteAttributeString("type", assemblyQualifiedName);

                writer.WriteStartElement("key");
                serializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                this.GetXmlSerializer(assemblyQualifiedName).Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private XmlSerializer GetXmlSerializer(string type)
        {
            if (this.XmlSerializer.ContainsKey(type))
            {
                return this.XmlSerializer[type];
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
                this.XmlSerializer[type] = serializer;
                return serializer;
            }
        }
    }
}
