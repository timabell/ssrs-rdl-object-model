using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about a parameter for the report.
	/// </summary>
	public class ReportParameter : IXmlSerializable
    {
        #region Constants
        public const string PARAM_METADATA                  = "Metadata";
        public const string PARAM_SETTINGS                  = "Settings";
        public const string PARAM_SETTINGS_DEFAULT_VALUE    = "<Settings/>";
        public const string PARAM_REPORT_PACKAGE_SETTINGS   = "ReportPackageSettings";
        public const string PARAM_ITEMS                     = "Items";
        public const string PARAM_CROSSTAB                  = "CrosstabReportParameters";
        public static string TITLE                          = "Title";
        public static string REPORT_TYPE                    = "ReportType";
        public static string SUMMARY_LEVELS                 = "SummaryLevels";
        public static string CUSTOM_FIELD1                  = "CustomField1";
        public static string CUSTOM_FIELD2                  = "CustomField2";
        public static string CUSTOM_FIELD3                  = "CustomField3";

        #endregion
        private string						_name;
		private DataType					_dataType;
		private bool						_nullable;
		private DefaultValue				_defaultValue;
		private bool						_allowBlank;
		private string						_prompt;
		private ValidValues					_validValues;
		private bool						_hidden;
		private bool						_multiValue;
		private UsedInQuery					_usedInQuery; 

		/// <summary>
		/// Creates a new instance of a ReportParameter.
		/// </summary>
		public ReportParameter(){}
        public ReportParameter(string name, string defaultValue, DataType dataType, bool hidden):this()
        {
            this._name = name;

            if (defaultValue != null)
            {
                this.DefaultValue = new DefaultValue(new ValueCollection(new string[] {defaultValue }));
                //ValueCollection values = new ValueCollection();
                //values.Add( new Expression(defaultValue));
                //dv.Values = values;
            }
            this.DataType = dataType;
            this._hidden = hidden;
        }

		/// <summary>
		/// Name of the parameter.
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// The data type of the parameter.
		/// </summary>
		public DataType DataType
		{
			get {return _dataType;}
			set {_dataType = value;}
		}

		/// <summary>
		/// Indicates the value for this parameter is allowed to be null.
		/// </summary>
		public bool Nullable
		{
			get {return _nullable;}
			set {_nullable = value;}
		}

		/// <summary>
		/// The default value to use for the parameter.
		/// </summary>
		public DefaultValue DefaultValue
		{
			get 
			{
				if (_defaultValue == null)
					_defaultValue = new DefaultValue();

				return _defaultValue;
			}

			set {_defaultValue = value;}
		}

		/// <summary>
		/// Indicates the value for this parameter is allowed to be an empty string
		/// </summary>
		public bool AllowBlank
		{
			get {return _allowBlank;}
			set {_allowBlank = value;}
		}

		/// <summary>
		/// The user prompt to display when asking for parameter values.
		/// </summary>
		public string Prompt
		{
			get {return _prompt;}
			set {_prompt = value;}
		}

		/// <summary>
		/// The possible values for the parameter.
		/// </summary>
		public ValidValues ValidValues
		{
			get 
			{
				if (_validValues == null)
					_validValues = new ValidValues();

				return _validValues;
			}

			set {_validValues = value;}
		}

		public bool Hidden
		{
			get { return _hidden; }
			set { _hidden = value; }
		}

		public bool MultiValue
		{
			get { return _multiValue; }
			set { _multiValue = value; }
		}

		/// <summary>
		/// Indicates whether or not the parameter is used in a query in the report.
		/// </summary>
		public UsedInQuery UsedInQuery
		{
			get {return _usedInQuery;}
			set {_usedInQuery = value;}
		}

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a ReportParameter into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ReportParameter is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- ReportParameter
			writer.WriteStartElement(Rdl.REPORTPARAMETER);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- DataType
			writer.WriteElementString(Rdl.DATATYPE, _dataType.ToString());

			//--- Nullable
			if(_nullable)
				writer.WriteElementString(Rdl.NULLABLE, _nullable.ToString().ToLower());

			//--- DefaultValue
			if(_defaultValue != null)
				((IXmlSerializable)_defaultValue).WriteXml(writer);

			//--- AllowBlank
			if(_allowBlank)
				writer.WriteElementString(Rdl.ALLOWBLANK, _allowBlank.ToString().ToLower());

			//--- Prompt
			if (!string.IsNullOrEmpty(_prompt))
				writer.WriteElementString(Rdl.PROMPT, _prompt.ToString());

			//--- ValidValues
			if (_validValues != null)
				((IXmlSerializable)_validValues).WriteXml(writer);

			//--- Hidden
			if (_hidden)
				writer.WriteElementString(Rdl.HIDDEN, _hidden.ToString().ToLower());

			//--- MultiValue
			if (_multiValue)
				writer.WriteElementString(Rdl.MULTIVALUE, _multiValue.ToString().ToLower());

			//--- UsedInQuery
			if(_usedInQuery != UsedInQuery.Auto)
				writer.WriteElementString(Rdl.USEDINQUERY, _usedInQuery.ToString());

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
		/// Generates an ReportParameter from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ReportParameter is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			//--- Name
			if(reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.REPORTPARAMETER)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- DataType
					if (reader.Name == Rdl.DATATYPE)
						_dataType = (DataType)Enum.Parse(typeof(DataType), reader.ReadString());

					//--- Nullable
					if (reader.Name == Rdl.NULLABLE)
						_nullable = bool.Parse(reader.ReadString());

					//--- DefaultValue
					if (reader.Name == Rdl.DEFAULTVALUE)
					{
						if (_defaultValue == null)
							_defaultValue = new DefaultValue();

						((IXmlSerializable)_defaultValue).ReadXml(reader);
					}

					//--- AllowBlank
					if (reader.Name == Rdl.ALLOWBLANK)
						_allowBlank = bool.Parse(reader.ReadString());

					//--- Prompt
					if (reader.Name == Rdl.PROMPT)
						_prompt = reader.ReadString();
					
					//--- ValidValues
					if (reader.Name == Rdl.VALIDVALUES && !reader.IsEmptyElement)
					{
						if (_validValues == null)
							_validValues = new ValidValues();

						((IXmlSerializable)_validValues).ReadXml(reader);
					}

					//--- Hidden
					if (reader.Name == Rdl.HIDDEN)
						_hidden = bool.Parse(reader.ReadString());

					//--- MultiValue
					if (reader.Name == Rdl.MULTIVALUE)
						_multiValue = bool.Parse(reader.ReadString());

					//--- UsedInQuery
					if (reader.Name == Rdl.USEDINQUERY)
						_usedInQuery = (UsedInQuery)Enum.Parse(typeof(UsedInQuery), reader.ReadString());
				}
			}
		}

		#endregion
	}
}
