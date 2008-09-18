using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the contents of a collection of cells in a table data region.
	/// </summary>
	public class TableCellCollection : Collection<TableCell>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a TableCellCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableCellCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.TABLECELLS);

			foreach (IXmlSerializable tableCell in Items)
				tableCell.WriteXml(writer);

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
		/// Generates an TableCellCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableCellCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.TABLECELLS)
			{
				TableCell tableCell = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLECELLS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- TableCell
						if (reader.LocalName == Rdl.TABLECELL)
						{
							tableCell = new TableCell();

							((IXmlSerializable)tableCell).ReadXml(reader);

							Add(tableCell);
						}
					}
				}
			}
		}

		#endregion
	}
}


