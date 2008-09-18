using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about a parameter.
	/// </summary>
	public class Parameter : IXmlSerializable
    {
        #region Constants

        #endregion 

        #region Private Variables

        private string		_name;
		private Expression	_value;
		private Expression	_omit;

		#endregion

		/// <summary>
		/// Creates an instance of a Parameter class.
		/// </summary>
		public Parameter() { }

		#region Public Properties

		/// <summary>
		/// Name to use for the field within the report.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// An expression that evaluates to the value to hand in for the 
		/// parameter to the subreport.
		/// </summary>
		public Expression Value
		{
			get
			{
				if (_value == null)
					_value = new Value();

				return _value;
			}
			set { _value = value; }
		}

		/// <summary>
		/// Indicates the parameter should be skipped.
		/// </summary>
		public Expression Omit
		{
			get
			{
				if (_omit == null)
					_omit = new Value();

				return _omit;
			}
			set { _omit = value; }
		}

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a Parameter into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Parameter is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Parameter
			writer.WriteStartElement(Rdl.PARAMETER);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- Value
			if (_value != null)
				writer.WriteElementString(Rdl.VALUE, _value.Value.ToString());

			//--- Omit
			if (_omit != null)
				writer.WriteElementString(Rdl.OMIT, _omit.Value.ToString());

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
		/// Generates an Parameter from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Parameter is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.PARAMETER)
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

					//--- Omit
					if (reader.Name == Rdl.OMIT)
					{
						if (_omit == null)
							_omit = new Expression();

						_omit.Value = reader.ReadString();
					}
				}
			}
		}

		#endregion

	}
}
