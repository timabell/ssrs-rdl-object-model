using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about a data source.
	/// </summary>
	public class DataSource : IXmlSerializable
	{
		#region Private Variables

		private string					_name;
		private bool					_transaction;
		private ConnectionProperties	_connectionProperties;
		private string					_dataSourceReference;

		#endregion

		/// <summary>
		/// Creates an instance of a DataSource.
		/// </summary>
		public DataSource(){}

		/// <summary>
		/// Creates an instance of a DataSource with the given name.
		/// </summary>
		/// <param name="name">The name of the data source.</param>
		public DataSource(string name)
		{
			_name = name;
		}

		#region Public Properties

		/// <summary>
		/// The name of the data source.
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// Indicates that data sets that use this data source should be executed
		/// in a single transaction.
		/// </summary>
		public bool Transaction
		{
			get {return _transaction;}
			set {_transaction = value;}
		}

		/// <summary>
		/// Information about how to connect to the data source.
		/// </summary>
		public ConnectionProperties ConnectionProperties
		{
			get {return _connectionProperties;}
			set {_connectionProperties = value;}
		}
		
		/// <summary>
		/// The full or relative path to a data source reference.
		/// </summary>
		public string DataSourceReference
		{
			get {return _dataSourceReference;}
			set {_dataSourceReference = value;}
		}

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a DataSource into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataSet is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- ReportParameter
			writer.WriteStartElement(Rdl.DATASOURCE);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- Transaction
			if (_transaction)
				writer.WriteElementString(Rdl.TRANSACTION, _transaction.ToString().ToLower());

			//--- ConnectionProperties
			if (_connectionProperties != null)
				((IXmlSerializable)_connectionProperties).WriteXml(writer);

			//--- DataSourceReference
			if (_dataSourceReference.Trim() != string.Empty)
				writer.WriteElementString(Rdl.DATASOURCEREFERENCE, _dataSourceReference);

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
		/// Generates an DataSource from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataSource is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATASOURCE)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Transaction
					if (reader.Name == Rdl.TRANSACTION)
						_transaction = bool.Parse(reader.ReadString());

					//--- ConnectionProperties
					if (reader.Name == Rdl.CONNECTIONPROPERTIES)
					{
						if (_connectionProperties == null)
							_connectionProperties = new ConnectionProperties();

						((IXmlSerializable)_connectionProperties).ReadXml(reader);
					}

					//--- DataSourceReference
					if (reader.Name == Rdl.DATASOURCEREFERENCE)
						_dataSourceReference = reader.ReadString();
				}
			}
		}

		#endregion
}
}
