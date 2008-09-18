using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about a set of data retrieved as a part of the report.
	/// </summary>
	public class DataSet : IXmlSerializable
    {
        #region Constants
        public const string     MAIN_DATASET_NAME = "dsMain";
        #endregion

        #region Private Variables

        private string				_name;
		private FieldCollection		_fields;
		private Query				_query;
		private Sensitivity			_caseSensitivity;
		private string				_collation;
		private Sensitivity			_accentSensitivity;
		private Sensitivity			_kanatypeSensitivity;
		private Sensitivity			_widthSensitivity;
		private FilterCollection	_filters;

		#endregion

		/// <summary>
		/// Creates an instance of a DataSet.
		/// </summary>
		public DataSet(){}

		/// <summary>
		/// Creates an instance of a DataSet with the given name.
		/// </summary>
		/// <param name="name">The name of the DataSet.</param>
		public DataSet(string name):this()
		{
			_name = name;
		}


		#region Public Properties

		/// <summary>
		/// Name of the data set.
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// The fields in the data set.
		/// </summary>
		public FieldCollection Fields
		{
			get 
			{
				if(_fields == null)
					_fields = new FieldCollection();

				return _fields;
			}

			set {_fields = value;}
		}

		/// <summary>
		/// Information about the data source, including connection information, query, etc.
		/// </summary>
		public Query Query
		{
			get 
			{
				if (_query == null)
					_query = new Query();

				return _query;
			}
			set {_query = value;}
		}

		/// <summary>
		/// Indicates if the data is case sensitive.
		/// </summary>
		public Sensitivity CaseSensitivity
		{
			get {return _caseSensitivity;}
			set {_caseSensitivity = value;}
		}

		/// <summary>
		/// The locale to use for the collation sequence for sorting data.
		/// </summary>
		public string Collation
		{
			get {return _collation;}
			set {_collation = value;}
		}

		/// <summary>
		/// Indicates whether the data is accent sensitive.
		/// </summary>
		public Sensitivity AccentSensitivity
		{
			get {return _accentSensitivity;}
			set {_accentSensitivity = value;}
		}

		/// <summary>
		/// Indicates whether the data is kanatype sensitive.
		/// </summary>
		public Sensitivity KanatypeSensitivity
		{
			get {return _kanatypeSensitivity;}
			set {_kanatypeSensitivity = value;}
		}

		/// <summary>
		/// Indicates whether the data is width sensitive.
		/// </summary>
		public Sensitivity WidthSensitivity
		{
			get {return _widthSensitivity;}
			set {_widthSensitivity = value;}
		}

		/// <summary>
		/// Filters to apply to each row of data in the data set.
		/// </summary>
		public FilterCollection Filters
		{
			get 
			{
				if (_filters == null)
					_filters = new FilterCollection();

				return _filters;
			}

			set {_filters = value;}
		}

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a DataSet into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataSet is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- DataSet
			writer.WriteStartElement(Rdl.DATASET);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- Fields
			if(_fields != null)
				((IXmlSerializable)_fields).WriteXml(writer);

			//--- Query
			if(_query != null)
				((IXmlSerializable)_query).WriteXml(writer);

			//--- CaseSensitivity
			if(_caseSensitivity != Sensitivity.Auto)
				writer.WriteElementString(Rdl.CASESENSITIVITY, _caseSensitivity.ToString());

			//--- Collation
			writer.WriteElementString(Rdl.COLLATION, _collation);

			//--- AccentSensitivity
			if(_accentSensitivity != Sensitivity.Auto)
				writer.WriteElementString(Rdl.ACCENTSENSITIVITY, _accentSensitivity.ToString());

			//--- KanatypeSensitivity
			if(_kanatypeSensitivity != Sensitivity.Auto)
				writer.WriteElementString(Rdl.KANATYPESENSITIVITY, _kanatypeSensitivity.ToString());

			//--- WidthSensititvity
			if(_widthSensitivity != Sensitivity.Auto)
				writer.WriteElementString(Rdl.WIDTHSENSITIVITY, _widthSensitivity.ToString());

			//--- Filters
			if(_filters != null)
				((IXmlSerializable)_filters).WriteXml(writer);

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
		/// Generates an DataSet from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataSet is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATASET)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Fields
					if (reader.Name == Rdl.FIELDS)
					{
						if (_fields == null)
							_fields = new FieldCollection();

						((IXmlSerializable)_fields).ReadXml(reader);
					}

					//--- Query
					if (reader.Name == Rdl.QUERY)
					{
						if (_query == null)
							_query = new Query();

						((IXmlSerializable)_query).ReadXml(reader);
					}

					//--- CaseSensitivity
					if (reader.Name == Rdl.CASESENSITIVITY)
						_caseSensitivity = (Sensitivity)Enum.Parse(typeof(Sensitivity), reader.ReadString());

					//--- Collation
					if (reader.Name == Rdl.COLLATION)
						_collation = reader.ReadString();

					//--- AccentSensitivity
					if (reader.Name == Rdl.ACCENTSENSITIVITY)
						_accentSensitivity = (Sensitivity)Enum.Parse(typeof(Sensitivity), reader.ReadString());

					//--- KanatypeSensitivity
					if (reader.Name == Rdl.KANATYPESENSITIVITY)
						_kanatypeSensitivity = (Sensitivity)Enum.Parse(typeof(Sensitivity), reader.ReadString());

					//--- WidthSensitivity
					if (reader.Name == Rdl.CASESENSITIVITY)
						_widthSensitivity = (Sensitivity)Enum.Parse(typeof(Sensitivity), reader.ReadString());

					//--- Filters
					if (reader.Name == Rdl.FILTERS && !reader.IsEmptyElement)
					{
						if (_filters == null)
							_filters = new FilterCollection();

						((IXmlSerializable)_filters).ReadXml(reader);
					}
				}
			}
		}

		#endregion

        #region Report Generation
        internal void Clear()
        {
            this.Fields.Clear();
        }
        #endregion
    }
}
