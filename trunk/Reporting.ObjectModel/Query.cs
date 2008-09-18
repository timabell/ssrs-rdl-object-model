using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains the description of the query to execute to retrie
	/// </summary>
	public class Query : IXmlSerializable
    {
        #region Constants
        
        //public const string QUERY_PREVIEW = "= String.Format(\"EXEC dbo.PreviewReport '{0}'\", Parameters!Metadata.Value)";
        //public const string QUERY_RUNTIME = "= String.Format(\"EXEC dbo.RunCustomReport '{0}', '{1}', {2}\", Parameters!Metadata.Value, Parameters!ReportMonth.Value, Parameters!Dataset.Value)";
            
        #endregion

        #region Private Variables

        private string						_dataSourceName;
		private CommandType					_commandType;
		private string						_commandText;
		private QueryParameterCollection	_queryParameters;
		private int							_timeOut;
        private bool                        _useGenericQueryDesigner;

		#endregion

		/// <summary>
		/// Creates a new instance of a Query
		/// </summary>
		public Query(){}
        public Query(string commandText ):this()
        {
            _commandText = commandText;
        }

		#region Public Properties

		/// <summary>
		/// Name of the data source to execute the query against.
		/// </summary>
		public string DataSourceName
		{
			get { return _dataSourceName; }
			set { _dataSourceName = value; }
		}

		/// <summary>
		/// Indicates what type of query is contained in the CommandText.
		/// </summary>
		public CommandType CommandType
		{
			get {return _commandType;}
			set {_commandType = value;}
		}

		/// <summary>
		/// The query to execute to obtain the data for the report.
		/// </summary>
		public string CommandText
		{
			get {return _commandText;}
			set {_commandText = value;}
		}
        /// <summary>
        /// Use Generic Query Designer
        /// </summary>
        public bool UseGenericQueryDesigner
        {
            get { return _useGenericQueryDesigner; }
            set { _useGenericQueryDesigner = value; }
        }

		/// <summary>
		/// List of parameters that are passed to the data source as part of the query.
		/// </summary>
		public QueryParameterCollection QueryParameters
		{
			get 
			{
				if (_queryParameters == null)
					_queryParameters = new QueryParameterCollection();

				return _queryParameters;
			}

			set {_queryParameters = value;}
		}

		/// <summary>
		/// Number of seconds to allow the query to run before timing out.
		/// </summary>
		public int TimeOut
		{
			get {return _timeOut;}
			set {_timeOut = value;}
		}

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a Query into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Query is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Query
			writer.WriteStartElement(Rdl.QUERY);

            // Adhoc reports shoudl be open in the Generic Query Designer
            writer.WriteElementString("rd:UseGenericDesigner", "True");

			//--- DataSourceName
			if (_dataSourceName.Trim() != string.Empty)
				writer.WriteElementString(Rdl.DATASOURCENAME, _dataSourceName);

			//--- CommandType
			if (_commandType != CommandType.Text)
				writer.WriteElementString(Rdl.COMMANDTYPE, _commandType.ToString());

			//--- CommandText
            if (_commandText!=null && _commandText.Trim() != string.Empty)
				writer.WriteElementString(Rdl.COMMANDTEXT, _commandText);

			//--- QueryParameters
			if (_queryParameters != null)
				((IXmlSerializable)_queryParameters).WriteXml(writer);

			//--- TimeOut
			writer.WriteElementString(Rdl.TIMEOUT, _timeOut.ToString());

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
		/// Generates an Query from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Query is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.QUERY)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- DataSourceName
					if (reader.Name == Rdl.DATASOURCENAME)
						_dataSourceName = reader.ReadString();

					//--- CommandType
					if (reader.Name == Rdl.COMMANDTYPE)
						_commandType = (CommandType)Enum.Parse(typeof(CommandType), reader.ReadString());

					//--- CommandText
					if (reader.Name == Rdl.COMMANDTEXT)
						_commandText = reader.ReadString();

					//--- QueryParameters
					if (reader.Name == Rdl.QUERYPARAMETERS)
					{
						if (_queryParameters == null)
							_queryParameters = new QueryParameterCollection();

						((IXmlSerializable)_queryParameters).ReadXml(reader);
					}

					//--- TimeOut
					if (reader.Name == Rdl.TIMEOUT)
						_timeOut = int.Parse(reader.ReadString());
				}
			}
		}

		#endregion

	}
}
