using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Represents a default value for a parameter.
	/// </summary>
	public class DefaultValue : IXmlSerializable
	{
		private DataSetReference		_dataSetReference;
		private ValueCollection			_values;

		/// <summary>
		/// Creates a new instance of a DefaultValue.
		/// </summary>
		public DefaultValue(){}
        public DefaultValue(ValueCollection values):this()
        {
            this.Values = values;
        }

		/// <summary>
		/// The query to execute to obtain the default value(s) for the parameter.
		/// </summary>
		public DataSetReference DataSetReference
		{
			get 
			{
				if (_dataSetReference == null)
					_dataSetReference = new DataSetReference();

				return _dataSetReference; 
			}

			set { _dataSetReference = value; }
		}

		/// <summary>
		/// The default values for the parameter.
		/// </summary>
		public ValueCollection Values
		{
			get 
			{
				if (_values == null)
					_values = new ValueCollection();

				return _values; 
			}

			set { _values = value; }
		}

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a DefaultValue into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DefaultValue is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.DEFAULTVALUE);

			//--- DataSetReference
			if (_dataSetReference != null)
				((IXmlSerializable)_dataSetReference).WriteXml(writer);

			//--- Values
			if (_values != null)
				((IXmlSerializable)_values).WriteXml(writer);

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
		/// Generates an DefaultValue from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DefaultValue is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DEFAULTVALUE)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- DataSetReference
					if (reader.Name == Rdl.DATASETREFERENCE)
					{
						if (_dataSetReference == null)
							_dataSetReference = new DataSetReference();

						((IXmlSerializable)_dataSetReference).ReadXml(reader);
					}

					//--- Values
					if (reader.Name == Rdl.VALUES)
					{
						if (_values == null)
							_values = new ValueCollection();

						((IXmlSerializable)_values).ReadXml(reader);
					}
				}
			}
		}

		#endregion
	}
}
