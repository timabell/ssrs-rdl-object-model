using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Collection of values to compare against in a filter.
	/// </summary>
	public class FilterValueCollection : Collection<FilterValue>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a FilterValueCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the FilterValueCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.FILTERVALUES);

			foreach (IXmlSerializable filterValue in Items)
				filterValue.WriteXml(writer);

			writer.WriteEndElement();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			// TODO:  Add GetSchema implementation
			return null;
		}

		/// <summary>
		/// Generates an FilterValueCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the FilterValueCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.FILTERVALUES)
			{
				FilterValue filterValue = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.FILTERVALUES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- FilterValue
						if (reader.LocalName == Rdl.FILTERVALUE)
						{
							filterValue = new FilterValue();

							((IXmlSerializable)filterValue).ReadXml(reader);

							Add(filterValue);
						}
					}
				}
			}
		}

		#endregion
	}
}


