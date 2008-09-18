using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains parameters that are passed to the data source as part of the query.
	/// </summary>
	public class QueryParameterCollection : Collection<QueryParameter>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a QueryParameterCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the QueryParameterCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.QUERYPARAMETERS);

			foreach (IXmlSerializable queryParameter in Items)
				queryParameter.WriteXml(writer);

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
		/// Generates an QueryParameterCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the QueryParameterCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.QUERYPARAMETERS)
			{
				QueryParameter queryParameter = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.QUERYPARAMETERS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- QueryParameter
						if (reader.LocalName == Rdl.QUERYPARAMETER)
						{
							queryParameter = new QueryParameter();

							((IXmlSerializable)queryParameter).ReadXml(reader);

							Add(queryParameter);
						}
					}
				}
			}
		}

		#endregion
	}
}


