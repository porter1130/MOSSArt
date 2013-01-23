using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Xml;
using Microsoft.SharePoint.Utilities;

namespace MOSSArt.CAMLQuery
{
    public class SingleCAMLExpression<T> : CAMLExpression<T>
    {
        public readonly string FieldName;

        public readonly string Operator;

        public readonly object Value;

        private SPFieldType _spFieldType = SPFieldType.Text;

        private Type _valueType = typeof(Object);

        public Type ValueType
        {
            set
            {
                if (value == typeof(String) || value == typeof(Object))
                    _spFieldType = SPFieldType.Text;
                else if (value == typeof(Int32) || value == typeof(Int16) || value == typeof(Int64))
                    _spFieldType = SPFieldType.Integer;
                else if (value == typeof(Decimal) || value == typeof(float) || value == typeof(Single))
                    _spFieldType = SPFieldType.Number;
                else if (value == typeof(Boolean))
                    _spFieldType = SPFieldType.Boolean;
                else if (value == typeof(DateTime))
                    _spFieldType = SPFieldType.DateTime;
                else if (value == typeof(SPUser))
                    _spFieldType = SPFieldType.User;
            }
        }

        public SingleCAMLExpression(string field, string op, object value)
        {
            FieldName = field;

            Operator = op;
            Value = value;
        }

        public SingleCAMLExpression(string field, string op, object value, Type valueType)
            : this(field, op, value)
        {
            ValueType = valueType;
        }


        #region ICAMLExpression Members
        public override void ToCAML(IFieldInternalNameProvider provider, System.Xml.XmlNode parentNode)
        {
            XmlElement node = parentNode.OwnerDocument.CreateElement(Operator);
            parentNode.AppendChild(node);

            XmlNode fieldNode = parentNode.OwnerDocument.CreateElement("FieldRef");
            node.AppendChild(fieldNode);

            XmlAttribute attribute = parentNode.OwnerDocument.CreateAttribute("Name");

            if (provider == null)
                attribute.Value = FieldName;
            else
            {
                attribute.Value = provider.GetInternalName(FieldName);
            }

            fieldNode.Attributes.Append(attribute);

            XmlNode valueNode = parentNode.OwnerDocument.CreateElement("Value");
            node.AppendChild(valueNode);

            switch (_spFieldType)
            {
                case SPFieldType.DateTime:
                    if (this.Value is DateTime)
                    {
                        valueNode.InnerText = SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(Value.ToString()));
                    }
                    else
                    {
                        valueNode.InnerText = this.Value.ToString();
                    }
                    break;
                case SPFieldType.User:
                    attribute = parentNode.OwnerDocument.CreateAttribute("LookupId");
                    attribute.Value = "TRUE";
                    fieldNode.Attributes.Append(attribute);

                    if (Value is SPUser)
                    {
                        valueNode.InnerText = ((SPUser)Value).ID.ToString();
                    }
                    else if (this.Value is QuerySchema.UserID)
                    {
                        valueNode.InnerXml = this.Value.ToString();
                    }
                    else
                    {
                        valueNode.InnerText = this.Value.ToString();
                    }
                    break;
                default:
                    valueNode.InnerText = Value.ToString();
                    break;
            }


            XmlAttribute att2 = parentNode.OwnerDocument.CreateAttribute("Type");
            att2.Value = _spFieldType.ToString();
            valueNode.Attributes.Append(att2);

        }

        #endregion
    }
}
