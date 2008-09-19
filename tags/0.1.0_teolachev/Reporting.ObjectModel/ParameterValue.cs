using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// A possible value for the parameter.
	/// </summary>
	public class ParameterValue : IXmlSerializable
	{
		private Expression	_value;
		private Expression _label;

		/// <summary>
		/// Creates a new instance of a ParameterValue.
		/// </summary>
		public ParameterValue(){}

		/// <summary>
		/// Possible value for the parameter.
		/// </summary>
		public Expression Value
		{
			get {return _value;}
			set {_value = value;}
		}

		/// <summary>
		/// Label for the value to display in the UI.
		/// </summary>
		public Expression Label
		{
			get {return _label;}
			set {_label = value;}
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
		/// Generates an ParameterValue from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ParameterValue is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.PARAMETERVALUE)
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

						_value.Value = reader.ReadString() ;
					}

					//--- Label
					if (reader.Name == Rdl.LABEL)
					{
						if (_label == null)
							_label = new Expression();

						_label.Value = reader.ReadString();
					}
				}
			}
		}

		/// <summary>
		/// Converts a ParameterValue into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ParameterValue is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- ParameterValue
			writer.WriteStartElement(Rdl.PARAMETERVALUE);

			//--- Value
			if (_value != null)
				writer.WriteElementString(Rdl.VALUE, _value.Value.ToString());

			//--- Label
			if (_label != null)
				writer.WriteElementString(Rdl.LABEL, _label.Value.ToString());

			writer.WriteEndElement();
		}

		#endregion
	}
}
