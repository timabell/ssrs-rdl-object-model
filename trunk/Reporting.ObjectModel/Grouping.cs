using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the expressions to group the data by.
	/// </summary>
	public class Grouping : IXmlSerializable
    {
        #region Constants
        internal const string GROUP_ACCOUNT_TYPE = "AccountType";
        internal const string GROUP_PREFIX = "grp";
        internal const string GROUP_LEVEL = "Level";
        #endregion
        #region Private Variables

        private string						_name;
		private Expression					_label;
		private GroupExpressionCollection	_groupExpressions;
		private bool						_pageBreakAtStart;
		private bool						_pageBreakAtEnd;
		private CustomPropertiesCollection	_customProperties;
		private FilterCollection			_filters;
		private Expression					_parent;
		private string						_dataElementName;
		private string						_dataCollectionName;
		private GroupingDataElementOutput	_dataElementOutput;

		#endregion

		/// <summary>
		/// Creates an instance of the Grouping class.
		/// </summary>
		public Grouping(){}

		#region Public Properties

		/// <summary>
		/// Name of the Grouping (for use in
		/// RunningValue and RowNumber).
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// A label to identify an instance of the group
		/// within the client UI (to provide a userfriendly
		/// label for searching).
		/// </summary>
		public Expression Label
		{
			get
			{
				if (_label == null)
					_label = new Expression();

				return _label;
			}
			set { _label = value; }
		}

		/// <summary>
		/// The expressions to group the data by.
		/// </summary>
		public GroupExpressionCollection GroupExpressions
		{
			get 
			{
				if(_groupExpressions == null)
					_groupExpressions = new GroupExpressionCollection();

				return _groupExpressions;
			}

			set {_groupExpressions = value;}
		}

		/// <summary>
		/// Indicates the report should page break at
		/// the start of the group.
		/// </summary>
		public bool PageBreakAtStart
		{
			get {return _pageBreakAtStart;}
			set {_pageBreakAtStart = value;}
		}

		/// <summary>
		/// Indicates the report should page break at
		/// the end of the group.
		/// </summary>
		public bool PageBreakAtEnd
		{
			get {return _pageBreakAtEnd;}
			set {_pageBreakAtEnd = value;}
		}


		public CustomPropertiesCollection CustomProperties
		{
			get
			{
				if (_customProperties == null)
					_customProperties = new CustomPropertiesCollection();

				return _customProperties;
			}
			set { _customProperties = value; }
		}

		/// <summary>
		/// Filters to apply to each instance of the
		/// group.
		/// </summary>
		public FilterCollection Filters
		{
			get
			{
				if (_filters == null)
					_filters = new FilterCollection();

				return _filters;
			}
			set { _filters = value; }
		}

		/// <summary>
		/// An expression that identifies the parent
		/// group in a recursive hierarchy.
		/// </summary>
		public Expression Parent
		{
			get
			{
				if (_parent == null)
					_parent = new Expression();

				return _parent;
			}
			set { _parent = value; }
		}

		/// <summary>
		/// The name to use for the data element for
		/// instances of this group.
		/// </summary>
		public string DataElementName
		{
			get {return _dataElementName;}
			set {_dataElementName = value;}
		}

		/// <summary>
		/// The name to use for the data element for
		/// the collection of all instances of this group.
		/// </summary>
		public string DataCollectionName
		{
			get {return _dataCollectionName;}
			set {_dataCollectionName = value;}
		}

		/// <summary>
		/// Indicates whether the group should appear
		/// in a data rendering.
		/// </summary>
		public GroupingDataElementOutput DataElementOutput
		{
			get {return _dataElementOutput;}
			set {_dataElementOutput = value;}
		}

		#endregion

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
		/// Generates an Grouping from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Grouping is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.GROUPING)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Label
					if (reader.Name == Rdl.LABEL)
					{
						if (_label == null)
							_label = new Expression();

						_label.Value = reader.ReadString();
					}

					//--- GroupExpressions
					if (reader.Name == Rdl.GROUPEXPRESSIONS)
					{
						if (_groupExpressions == null)
							_groupExpressions = new GroupExpressionCollection();

						((IXmlSerializable)_groupExpressions).ReadXml(reader);
					}

					//--- PageBreakAtStart
					if (reader.Name == Rdl.PAGEBREAKATSTART)
						_pageBreakAtStart = bool.Parse(reader.ReadString());

					//--- PageBreakAtEnd
					if (reader.Name == Rdl.PAGEBREAKATEND)
						_pageBreakAtEnd = bool.Parse(reader.ReadString());

					//--- CustomProperties
					if (reader.Name == Rdl.CUSTOMPROPERTIES)
					{
						if (_customProperties == null)
							_customProperties = new CustomPropertiesCollection();

						((IXmlSerializable)_customProperties).ReadXml(reader);
					}

					//--- Filters
					if (reader.Name == Rdl.FILTERS)
					{
						if (_filters == null)
							_filters = new FilterCollection();

						((IXmlSerializable)_filters).ReadXml(reader);
					}

					//--- Parent
					if (reader.Name == Rdl.PARENT)
					{
						if (_parent == null)
							_parent = new Expression();

						_parent.Value = reader.ReadString();
					}

					//--- DataElementName
					if (reader.Name == Rdl.DATAELEMENTNAME)
						_dataElementName = reader.ReadString();

					//--- DataCollectionName
					if (reader.Name == Rdl.DATACOLLECTIONNAME)
						_dataCollectionName = reader.ReadString();

					//--- DataElementOutput
					if (reader.Name == Rdl.DATAELEMENTOUTPUT)
						_dataElementOutput = (GroupingDataElementOutput)Enum.Parse(typeof(GroupingDataElementOutput), reader.ReadString());
				}
			}
		}

		/// <summary>
		/// Converts a Grouping into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Grouping is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Grouping
			writer.WriteStartElement(Rdl.GROUPING);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- Label
			if (_label != null)
				writer.WriteElementString(Rdl.LABEL, _label.Value.ToString());

			//--- GroupExpressions
			if (_groupExpressions != null)
				((IXmlSerializable)_groupExpressions).WriteXml(writer);

			//--- PageBreakAtStart
			if(_pageBreakAtStart)
				writer.WriteElementString(Rdl.PAGEBREAKATSTART, _pageBreakAtStart.ToString().ToLower());

			//--- PageBreakAtEnd
			if(_pageBreakAtEnd)
				writer.WriteElementString(Rdl.PAGEBREAKATEND, _pageBreakAtEnd.ToString().ToLower());

			//--- CustomProperties
			if (_customProperties != null)
				((IXmlSerializable)_customProperties).WriteXml(writer);

			//--- Filters
			if (_filters != null)
				((IXmlSerializable)_filters).WriteXml(writer);

			//--- Parent
			if (_parent != null)
				writer.WriteElementString(Rdl.PARENT, _parent.Value.ToString());

			//--- DataElementName
			if(_dataElementName != null && _dataElementName != string.Empty)
				writer.WriteElementString(Rdl.DATAELEMENTNAME, _dataElementName);

			//--- DataCollectionName
			if (_dataCollectionName != null && _dataCollectionName != string.Empty)
				writer.WriteElementString(Rdl.DATACOLLECTIONNAME, _dataCollectionName);

			//--- DataElementOutput
			if(_dataElementOutput != GroupingDataElementOutput.Output)
				writer.WriteElementString(Rdl.DATAELEMENTOUTPUT, _dataElementOutput.ToString());

			writer.WriteEndElement();
		}

		#endregion
	}
}
