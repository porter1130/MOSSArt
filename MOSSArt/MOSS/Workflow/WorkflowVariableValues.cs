using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace MOSSArt
{
    [Serializable, XmlRoot("Data")]
    public class WorkflowVariableValues : SerializableDictionary<string, object>
    {
        public WorkflowVariableValues() { }
       
    }
}
