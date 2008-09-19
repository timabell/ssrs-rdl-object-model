using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	public class CustomProperty : IXmlSerializable
	{
		private string _name;
		private string _value;

		public CustomProperty() { }

		public CustomProperty(string name, string value) 
		{
			_name = name;
			_value = value;
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.CUSTOMPROPERTY)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Name
					if (reader.Name == Rdl.NAME)
						_name = reader.ReadString();

					//--- Value
					if (reader.Name == Rdl.VALUE)
						_value = reader.ReadString();
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			//--- CustomProperty
			writer.WriteStartElement(Rdl.CUSTOMPROPERTY);

			//--- Name
			writer.WriteElementString(Rdl.NAME, _name);

			//--- Value
			writer.WriteElementString(Rdl.VALUE, _value);

			writer.WriteEndElement();
		}

		#endregion
	}
}
