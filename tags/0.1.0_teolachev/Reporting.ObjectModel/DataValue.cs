using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines a single value for the DataPoint in the chart.
	/// </summary>
	public class DataValue : IXmlSerializable
	{
		private Expression _value;

		/// <summary>
		/// Creates an instance of the DataValue class.
		/// </summary>
		public DataValue() { }

		/// <summary>
		/// Value expression.
		/// </summary>
		public Expression Value
		{
			get
			{
				if (_value == null)
					_value = new Expression();

				return _value;
			}
			set { _value = value; }
		}

		#region IXmlSerializable Members

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Generates an DataValue from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataValue is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATAVALUE)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Value
					if (reader.Name == Rdl.VALUE)
					{
						if (_value == null)
							_value = new Expression();

						_value.Value = reader.ReadString();
					}
				}
			}
		}

		/// <summary>
		/// Converts a DataValue into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataValue is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- DataValue
			writer.WriteStartElement(Rdl.DATAVALUE);

			//--- Value
			if (_value != null)
				writer.WriteElementString(Rdl.VALUE, _value.Value.ToString());

			writer.WriteEndElement();
		}

		#endregion
	}
}
