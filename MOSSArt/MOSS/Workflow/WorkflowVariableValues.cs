using System;
using System.Xml.Serialization;

namespace MOSSArt
{
    [Serializable, XmlRoot("Data")]
    public class WorkflowVariableValues : SerializableDictionary<string, object>
    {
    }
}
