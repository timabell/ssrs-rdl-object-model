using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// The query to execute to obtain a list of values or default values for a parameter.
	/// </summary>
	public class DataSetReference : IXmlSerializable
	{
		private string _dataSetName;
		private string _valueField;
		private string _labelField;

		/// <summary>
		/// Creates a new instance of a DataSetReference.
		/// </summary>
		public DataSetReference() { }

		/// <summary>
		/// Name of the data set to use.
		/// </summary>
		public string DataSetName
		{
			get { return _dataSetName; }
			set { _dataSetName = value; }
		}

		/// <summary>
		/// Name of the field to use for the values/defaults for the parameter.
		/// </summary>
		public string ValueField
		{
			get { return _valueField; }
			set { _valueField = value; }
		}

		/// <summary>
		/// Name of the field to use for the value to display to the user for the selection.
		/// </summary>
		public string LabelField
		{
			get { return _labelField; }
			set { _labelField = value; }
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
		/// Generates an DataSetReference from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataSetReference is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATASETREFERENCE)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- DataSetName
					if (reader.Name == Rdl.DATASETNAME)
						_dataSetName = reader.ReadString();

					//--- ValueFiels
					if (reader.Name == Rdl.VALUEFIELD)
						_valueField = reader.ReadString();

					//--- LabelField
					if (reader.Name == Rdl.LABELFIELD)
						_labelField = reader.ReadString();
				}
			}
		}

		/// <summary>
		/// Converts a DataSetReference into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataSetReference is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- DataSetReference
			writer.WriteStartElement(Rdl.DATASETREFERENCE);

			//--- DataSetName
			writer.WriteElementString(Rdl.DATASETNAME, _dataSetName);

			//--- ValueField
			writer.WriteElementString(Rdl.VALUEFIELD, _valueField);

			//--- LabelField
			writer.WriteElementString(Rdl.LABELFIELD, _labelField);

			writer.WriteEndElement();
		}

		#endregion
	}
}
