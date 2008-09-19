using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the footer rows for a table or group.
	/// </summary>
	public class Footer : IXmlSerializable
    {
        #region Constants
        internal const string PREFIX = "f";
        internal const string REPORT_FOOTER = "ReportFooter";
        
        #endregion
        #region Private Variables

        private TableRowCollection		_tableRows;
		private bool					_repeatOnNewPage;

		#endregion

		/// <summary>
		/// Creates an instance of the Footer class.
		/// </summary>
		public Footer(){}

		#region Public Properties

		/// <summary>
		/// The footer rows for the table or group.
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
		/// Inidicates this footer should be displayed on each page that 
		/// the table (or group) is displayed.
		/// </summary>
		public bool RepeatOnNewPage
		{
			get {return _repeatOnNewPage;}
			set {_repeatOnNewPage = value;}
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
		/// Generates an Footer from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Footer is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.FOOTER)
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
		/// Converts a Footer into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Footer is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
            if (this.TableRows.Count == 0) return;

			//--- Header
			writer.WriteStartElement(Rdl.FOOTER);

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
