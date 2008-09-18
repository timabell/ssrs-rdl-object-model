using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the columns in the table.
	/// </summary>
	public class TableColumnCollection : Collection<TableColumn>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a TableColumnCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableColumnCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.TABLECOLUMNS);

			foreach (IXmlSerializable tableColumn in Items)
				tableColumn.WriteXml(writer);

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
		/// Generates an TableColumnCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableColumnCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.TABLECOLUMNS)
			{
				TableColumn tableColumn = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLECOLUMNS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- TableColumn
						if (reader.LocalName == Rdl.TABLECOLUMN)
						{
							tableColumn = new TableColumn();

							((IXmlSerializable)tableColumn).ReadXml(reader);

							Add(tableColumn);
						}
					}
				}
			}
		}

		#endregion
	}
}


