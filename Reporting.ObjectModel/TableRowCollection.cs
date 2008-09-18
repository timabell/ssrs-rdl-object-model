using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines an ordered list of table rows.
	/// </summary>
	public class TableRowCollection : Collection<TableRow>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a TableRowCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableRowCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.TABLEROWS);

			foreach (IXmlSerializable tableRow in Items)
				tableRow.WriteXml(writer);

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
		/// Generates an TableRowCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableRowCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.TABLEROWS)
			{
				TableRow tableRow = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLEROWS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- TableRow
						if (reader.LocalName == Rdl.TABLEROW)
						{
							tableRow = new TableRow();

							((IXmlSerializable)tableRow).ReadXml(reader);

							Add(tableRow);
						}
					}
				}
			}
		}

		#endregion
	}
}


