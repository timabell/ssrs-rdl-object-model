using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines a group in a table data region.
	/// </summary>
	public class TableGroup : IXmlSerializable
	{
		#region Private Variables

		private Grouping		_grouping;
		private Header			_header;
		private Footer			_footer;
		private Visibility		_visibility;

		#endregion

		/// <summary>
		/// Creates an instance of the TableGoup class.
		/// </summary>
		public TableGroup(){}

        internal TableGroup (string groupName, string groupExpression):this()
        {
            this.Grouping = new Grouping();
            this.Grouping.Name = groupName;
            this.Grouping.GroupExpressions = new GroupExpressionCollection();
            this.Grouping.GroupExpressions.Add(new Expression(groupExpression));

        }

		#region Public Properties

		/// <summary>
		/// The expressions to group the data by.
		/// </summary>
		public Grouping Grouping
		{
			get {return _grouping;}
			set {_grouping = value;}
		}


		/// <summary>
		/// A group header row.
		/// </summary>
		public Header Header
		{
			get {return _header;}
			set {_header = value;}
		}

		/// <summary>
		/// A group footer row.
		/// </summary>
		public Footer Footer
		{
			get {return _footer;}
			set {_footer = value;}
		}

		/// <summary>
		/// Indicates if the group should be hidden.
		/// </summary>
		public Visibility Visibility
		{
			get {return _visibility;}
			set {_visibility = value;}
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
		/// Generates an TableGroup from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableGroup is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLEGROUP)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Grouping
					if (reader.Name == Rdl.GROUPING)
					{
						if (_grouping == null)
							_grouping = new Grouping();

						((IXmlSerializable)_grouping).ReadXml(reader);
					}


					//--- Header
					if (reader.Name == Rdl.HEADER)
					{
						if (_header == null)
							_header = new Header();

						((IXmlSerializable)_header).ReadXml(reader);
					}

					//--- Footer
					if (reader.Name == Rdl.FOOTER)
					{
						if (_footer == null)
							_footer = new Footer();

						((IXmlSerializable)_footer).ReadXml(reader);
					}

					//--- Visibility
					if (reader.Name == Rdl.VISIBILITY)
					{
						if (_visibility == null)
							_visibility = new Visibility();

						((IXmlSerializable)_visibility).ReadXml(reader);
					}
				}
			}
		}

		/// <summary>
		/// Converts a TableGroup into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableGroup is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- TableGroup
			writer.WriteStartElement(Rdl.TABLEGROUP);

			//--- Grouping
			if (_grouping != null)
				((IXmlSerializable)_grouping).WriteXml(writer);


			//--- Header
			if (_header != null)
				((IXmlSerializable)_header).WriteXml(writer);

			//--- Footer
			if (_footer != null)
				((IXmlSerializable)_footer).WriteXml(writer);

			//--- Visibility
			if (_visibility != null)
				((IXmlSerializable)_visibility).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
}
}
