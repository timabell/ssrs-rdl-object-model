using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the detail rows for a table.
	/// </summary>
	public class Details : IXmlSerializable
	{
		#region Private Variables

		private TableRowCollection	_tableRows;
		private Grouping			_grouping;
		private Visibility			_visibility;

		#endregion

		/// <summary>
		/// Creates an instance of the Details class.
		/// </summary>
		public Details(){}

		#region Public Properties

		/// <summary>
		/// The detail rows for the table.
		/// </summary>
		public TableRowCollection TableRows
		{
			get 
			{
				if(_tableRows == null)
					_tableRows = new TableRowCollection();

				return _tableRows;
			}
		}

        /// <summary>
        /// Returns the table details height
        /// </summary>
        public Size Height
        {
            get
            {
                Size height = new Size(0);
                
                foreach (TableRow r in this.TableRows)
                {
                    height += r.Height;
                }
                return height;
            }
        }
		/// <summary>
		/// The expressions to group the detail data by.
		/// </summary>
		public Grouping Grouping
		{
			get
			{
				if (_grouping == null)
					_grouping = new Grouping();

				return _grouping;
			}
			set { _grouping = value; }
		}

		/// <summary>
		/// Inidicates if the details should be hidden.
		/// </summary>
		public Visibility Visibility
		{
			get
			{
				if (_visibility == null)
					_visibility = new Visibility();

				return _visibility;
			}
			set { _visibility = value; }
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
		/// Generates an Details from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Details is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DETAILS)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- TableRows
					if (reader.Name == Rdl.TABLEROWS)
					{
						if (_tableRows == null)
							_tableRows = new TableRowCollection();

						((IXmlSerializable)_tableRows).ReadXml(reader);
					}

					//--- Grouping
					if (reader.Name == Rdl.GROUPING)
					{
						if (_grouping == null)
							_grouping = new Grouping();

						((IXmlSerializable)_grouping).ReadXml(reader);
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
		/// Converts a Details into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Details is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Details
			writer.WriteStartElement(Rdl.DETAILS);

			//--- TableRows
			if (_tableRows != null)
				((IXmlSerializable)_tableRows).WriteXml(writer);

			//--- Grouping
			if (_grouping != null)
				((IXmlSerializable)_grouping).WriteXml(writer);


			//--- Visibility
			if (_visibility != null)
				((IXmlSerializable)_visibility).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
}
}
