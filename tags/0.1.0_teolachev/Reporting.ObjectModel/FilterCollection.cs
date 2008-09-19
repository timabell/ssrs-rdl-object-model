using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// An orderd list of filters used to restrict the rows in a data set
	/// or data region or to restrict the group instances in a grouping.
	/// </summary>
	public class FilterCollection : Collection<Filter>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a FilterCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the FilterCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
            if (this.Count == 0) return;

			writer.WriteStartElement(Rdl.FILTERS);

			foreach (IXmlSerializable filter in Items)
				filter.WriteXml(writer);

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
		/// Generates an FilterCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the FilterCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.FILTERS)
			{
				Filter filter = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.FILTERS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- Filter
						if (reader.LocalName == Rdl.FILTER)
						{
							filter = new Filter();

							((IXmlSerializable)filter).ReadXml(reader);

							Add(filter);
						}
					}
				}
			}
		}

		#endregion
	}
}


