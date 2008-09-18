using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class ParameterCollection : Collection<Parameter>, IXmlSerializable
	{
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a ParameterCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ParameterCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.PARAMETERS);

			foreach (IXmlSerializable parameter in Items)
				parameter.WriteXml(writer);

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
		/// Generates an ParameterCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ParameterCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.PARAMETERS)
			{
				Parameter parameter = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.PARAMETERS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- Parameter
						if (reader.LocalName == Rdl.PARAMETER)
						{
							parameter = new Parameter();

							((IXmlSerializable)parameter).ReadXml(reader);

							Add(parameter);
						}
					}
				}
			}
		}

		#endregion
	}
}


