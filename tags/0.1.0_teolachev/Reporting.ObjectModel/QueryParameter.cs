using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about a parameter that is passed to the data source
	/// as part of the query.
	/// </summary>
	public class QueryParameter : IXmlSerializable
	{
		#region Private Variables

		private string		_name;
		private Value		_value;

		#endregion

		/// <summary>
		/// Creates an instance of the QueryParameter class.
		/// </summary>
		public QueryParameter(){}

		#region Public Properties

		/// <summary>
		/// Name of the parameter.
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// An expression that evaluates to the value to hand to the data source.
		/// </summary>
		public Value Value
		{
			get 
			{
				if (_value == null)
					_value = new Value();

				return _value;
			}

			set {_value = value;}
		}

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a QueryParameter into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the QueryParameter is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- QueryParameter
			writer.WriteStartElement(Rdl.QUERYPARAMETER);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- Value
			if (_value != null)
				writer.WriteElementString(Rdl.VALUE, _value.Value.ToString());

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
		/// Generates an QueryParameter from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the QueryParameter is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.QUERYPARAMETER)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Value
					if (reader.Name == Rdl.VALUE)
					{
						if (_value == null)
							_value = new Value();

						_value.Value = reader.ReadString();
					}
				}
			}
		}

		#endregion

	}
}
