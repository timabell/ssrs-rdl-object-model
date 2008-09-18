using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the header rows for a table or group.
	/// </summary>
	public class Header : IXmlSerializable
    {
        #region Constants
        internal const string PREFIX = "h";
        public const double BORDER_GUTTER = 0.05;
        internal const string REPORT_HEADER = "ReportHeader";
        #endregion
        #region Private Variables

        private TableRowCollection		_tableRows;
		private bool					_repeatOnNewPage;

		#endregion

		/// <summary>
		/// Creates an instance of the Header class.
		/// </summary>
		public Header(){}

		#region Public Properties

		/// <summary>
		/// The header rows for the table or group.
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
        /// Returns the header height
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
		/// Indicates this header should be displayed on each page that 
		/// the table (or group) is displayed.
		/// </summary>
		public bool RepeatOnNewPage
		{
			get {return _repeatOnNewPage;}
			set {_repeatOnNewPage = value;}
		}

		#endregion

		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Generates an Header from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Header is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.HEADER)
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

					//--- RepeatOnNewPage
					if (reader.Name == Rdl.REPEATONNEWPAGE)
						_repeatOnNewPage = bool.Parse(reader.ReadString());
 				}
			}
		}

		/// <summary>
		/// Converts a Header into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Header is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Header
			writer.WriteStartElement(Rdl.HEADER);

			//--- TableRows
			if (_tableRows != null)
				((IXmlSerializable)_tableRows).WriteXml(writer);

			//--- RepeatOnNewPage
			writer.WriteElementString(Rdl.REPEATONNEWPAGE, _repeatOnNewPage.ToString().ToLower());

			writer.WriteEndElement();
		}

		#endregion
}
}
