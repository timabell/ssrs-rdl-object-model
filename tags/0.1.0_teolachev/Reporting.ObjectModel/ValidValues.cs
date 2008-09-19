using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// The possible values for a parameter.
	/// </summary>
	public class ValidValues : IXmlSerializable
	{
		private DataSetReference			_dataSetReference;
		private ParameterValueCollection	_parameterValues;

		/// <summary>
		/// Creates a new instance of a ValidValues.
		/// </summary>
		public ValidValues() { }

		/// <summary>
		/// The query to execute to obtain a list of possible values for the parameter.
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
		/// Hadrcoded values for the parameter.
		/// </summary>
		public ParameterValueCollection ParameterValues
		{
			get
			{
				if (_parameterValues == null)
					_parameterValues = new ParameterValueCollection();

				return _parameterValues;
			}
			set { _parameterValues = value; }
		}

		#region IXmlSerializable Members

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		/// <summary>
		/// Generates an ValidValues from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ValidValues is deserialized</param>
		public void ReadXml(System.Xml.XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.VALIDVALUES)
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

					//--- ParameterValues
					if (reader.Name == Rdl.PARAMETERVALUES)
					{
						if (_parameterValues == null)
							_parameterValues = new ParameterValueCollection();

						((IXmlSerializable)_parameterValues).ReadXml(reader);
					}		
				}
			}
		}

		/// <summary>
		/// Converts a ValidValues into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ValidValues is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- ValidValues
			writer.WriteStartElement(Rdl.VALIDVALUES);

			//--- DataSetReference
			if (_dataSetReference != null)
				((IXmlSerializable)_dataSetReference).WriteXml(writer);

			//--- ParameterValues
			if (_parameterValues != null)
				((IXmlSerializable)_parameterValues).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
}
}
