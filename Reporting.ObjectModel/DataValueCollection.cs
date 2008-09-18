using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class DataValueCollection : Collection<DataValue>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a DataValueCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataValueCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.DATAVALUES);

			foreach (IXmlSerializable dataValue in Items)
				dataValue.WriteXml(writer);

			writer.WriteEndElement();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Generates an DataValueCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataValueCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.DATAVALUES)
			{
				DataValue dataValue = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATAVALUES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- DataValue
						if (reader.LocalName == Rdl.DATAVALUE)
						{
							dataValue = new DataValue();

							((IXmlSerializable)dataValue).ReadXml(reader);

							Add(dataValue);
						}
					}
				}
			}
		}

		#endregion
	}
}


