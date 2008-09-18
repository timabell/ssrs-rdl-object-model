using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// The ordered list of possible values for a parameter.
	/// </summary>
	public class ParameterValueCollection : Collection<ParameterValue>, IXmlSerializable
	{
		#region IXmlSerializable Members

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
		/// Generates an ParameterValueCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ParameterValueCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.PARAMETERVALUES)
			{
				ParameterValue parameterValue = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.PARAMETERVALUES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- ParameterValue
						if (reader.LocalName == Rdl.PARAMETERVALUE)
						{
							parameterValue = new ParameterValue();

							((IXmlSerializable)parameterValue).ReadXml(reader);

							Add(parameterValue);
						}
					}
				}
			}		
		}

		/// <summary>
		/// Converts a ParameterValueCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ParameterValueCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.PARAMETERVALUES);

			foreach(IXmlSerializable parameterValue in Items)
				parameterValue.WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
	}
}

