using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about how to connect to a data source.
	/// </summary>
	public class ConnectionProperties : IXmlSerializable
	{
		#region Private Variables

		private string	_dataProvider;
		private string	_connectString;
		private bool	_integratedSecurity;
		private string	_prompt;

		#endregion

		/// <summary>
		/// Creates an instance of a ConnectionProperties class.
		/// </summary>
		public ConnectionProperties(){}

		#region Public Properties

		/// <summary>
		/// The type of the data source.
		/// </summary>
		public string DataProvider
		{
			get { return _dataProvider; }
			set { _dataProvider = value; }
		}

		/// <summary>
		/// The connection string for the data source.
		/// </summary>
		public string ConnectString
		{
			get { return _connectString; }
			set { _connectString = value; }
		}

		/// <summary>
		/// Indicates that this data source should be connected to using integrated security.
		/// </summary>
		public bool IntegratedSecurity
		{
			get { return _integratedSecurity; }
			set { _integratedSecurity = value; }
		}

		/// <summary>
		/// The prompt displayed to the user when prompting for database credentials
		/// for this data source.
		/// </summary>
		public string Prompt
		{
			get { return _prompt; }
			set { _prompt = value; }
		}

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a ConnectionProperties into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ConnectionProperties is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- ConnectionProperties
			writer.WriteStartElement(Rdl.CONNECTIONPROPERTIES);

			//--- DataProvider
			if (_dataProvider.Trim() != string.Empty)
				writer.WriteElementString(Rdl.DATAPROVIDER, _dataProvider);

			//--- ConnectString
			if (_connectString.Trim() != string.Empty)
				writer.WriteElementString(Rdl.CONNECTSTRING, _connectString);

			//--- IntegratedSecurity
			if (_integratedSecurity)
				writer.WriteElementString(Rdl.INTEGRATEDSECURITY, _integratedSecurity.ToString().ToLower());

			//--- Prompt
			if (_prompt.Trim() != string.Empty)
				writer.WriteElementString(Rdl.PROMPT, _prompt);

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
		/// Generates an ConnectionProperties from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ConnectionProperties is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					//--- DataProvider
					if (reader.Name == Rdl.DATAPROVIDER)
						_dataProvider = reader.ReadString();

					//--- ConnectString
					if (reader.Name == Rdl.CONNECTSTRING)
						_connectString = reader.ReadString();

					//--- IntegrateSecurity
					if (reader.Name == Rdl.INTEGRATEDSECURITY)
						_integratedSecurity = bool.Parse(reader.ReadString());

					//--- Prompt
					if (reader.Name == Rdl.PROMPT)
						_prompt = reader.ReadString();
				}
			}
		}

		#endregion
	}
}
