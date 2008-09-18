using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	public class CustomPropertiesCollection : Collection<CustomProperty>, IXmlSerializable
	{
		public CustomProperty this[string propertyName]
		{
			get
			{
				CustomProperty property = null;

				foreach (CustomProperty item in Items)
				{
					if (item.Name == propertyName)
					{
						property = item;
						break;
					}
				}

				if (property == null)
				{
					property = new CustomProperty(propertyName, string.Empty);
					Items.Add(property);
				}

				return property;
			}
		}

		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			if (reader.LocalName == Rdl.CUSTOMPROPERTIES)
			{
				CustomProperty customProperty = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.CUSTOMPROPERTIES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- CustomProperty
						if (reader.LocalName == Rdl.CUSTOMPROPERTY)
						{
							customProperty = new CustomProperty();

							((IXmlSerializable)customProperty).ReadXml(reader);

							Add(customProperty);
						}
					}
				}
			}
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
            if (this.Items.Count==0) return;
			writer.WriteStartElement(Rdl.CUSTOMPROPERTIES);

			foreach (IXmlSerializable customProperty in Items)
				customProperty.WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
	}
}
