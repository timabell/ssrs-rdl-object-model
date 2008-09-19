using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about how to connect to the sources of data for the various DataSets.
	/// </summary>
	public class DataSourceCollection : Collection<DataSource>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a DataSourceCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataSourceCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.DATASOURCES);

			foreach (IXmlSerializable dataSource in Items)
				dataSource.WriteXml(writer);

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
		/// Generates an DataSourceCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataSourceCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.DATASOURCES)
			{
				DataSource dataSource = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATASOURCES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- DataSet
						if (reader.LocalName == Rdl.DATASOURCE)
						{
							dataSource = new DataSource();

							((IXmlSerializable)dataSource).ReadXml(reader);

							Add(dataSource);
						}
					}
				}
			}
		}

		#endregion
	}
}


