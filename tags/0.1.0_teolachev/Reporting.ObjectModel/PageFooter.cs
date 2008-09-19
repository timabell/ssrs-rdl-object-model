using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the layout of report items to appear at the bottom of every page of the report.
	/// </summary>
	public class PageFooter : IXmlSerializable
	{
        public const string PAGE_FOOTER = "PageFooter";
		#region Private Variables

		private Size					_height;
		private bool					_printOnFirstPage = true;
		private bool					_printOnLastPage = true;
		private ReportItemCollection	_reportItems;
		private Style					_style;
        

		#endregion

        public PageFooter()
        {
        }
		/// <summary>
		/// Creates a new instance of a PageFooter.
		/// </summary>
		public PageFooter(Report report):this() 
        {
        }

		#region Public Properties

		/// <summary>
		/// Height of the page footer.
		/// </summary>
		public Size Height
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Indicates if the page footer should be shown on 
		/// the first page of the report.
		/// </summary>
		public bool PrintOnFirstPage
		{
			get { return _printOnFirstPage; }
			set { _printOnFirstPage = value; }
		}

		/// <summary>
		/// Indicates if the page footer should be shown on 
		/// the last page of the report.
		/// </summary>
		public bool PrintOnLastPage
		{
			get { return _printOnLastPage; }
			set { _printOnLastPage = value; }
		}

		/// <summary>
		/// The region that contains the elements of the footer layout.
		/// </summary>
		public ReportItemCollection ReportItems
		{
            get 
            {
                if (_reportItems == null)
                    _reportItems = new ReportItemCollection();

                return _reportItems; 
            }
			set { _reportItems = value; }
		}

		/// <summary>
		/// Style information of the page footer.
		/// </summary>
		public Style Style
		{
			get { return _style; }
			set { _style = value; }
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
		/// Generates an PageFooter from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the PageFooter is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.PAGEFOOTER)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Height
					if (reader.Name == Rdl.HEIGHT)
						_height = Size.Parse(reader.ReadString());

					//--- PrintOnFirstPage
					if (reader.Name == Rdl.PRINTONFIRSTPAGE)
						_printOnFirstPage = bool.Parse(reader.ReadString());

					//--- PrintOnLastPage
					if (reader.Name == Rdl.PRINTONLASTPAGE)
						_printOnLastPage = bool.Parse(reader.ReadString());

					//--- Report Items
					if (reader.Name == Rdl.REPORTITEMS && !reader.IsEmptyElement)
					{
						if (_reportItems == null)
							_reportItems = new ReportItemCollection();

						((IXmlSerializable)_reportItems).ReadXml(reader);
					}

					//--- Style
					if (reader.Name == Rdl.STYLE && !reader.IsEmptyElement)
					{
						if (_style == null)
							_style = new Style();

						((IXmlSerializable)_style).ReadXml(reader);
					}
				}
			}
		}

		/// <summary>
		/// Converts a PageFooter into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the PageFooter is serialized</param>
		public void WriteXml(XmlWriter writer)
		{

			//--- PageFooter
			writer.WriteStartElement(Rdl.PAGEFOOTER);

			//--- Height
			writer.WriteElementString(Rdl.HEIGHT, _height.ToString());

			//--- PrintOnFirstPage
			writer.WriteElementString(Rdl.PRINTONFIRSTPAGE, _printOnFirstPage.ToString().ToLower());

			//--- PrintOnLastPage
			writer.WriteElementString(Rdl.PRINTONLASTPAGE, _printOnLastPage.ToString().ToLower());

			//--- ReportItems
			if (_reportItems != null && _reportItems.Count > 0)
				((IXmlSerializable)_reportItems).WriteXml(writer);

			//---Style
			if(_style != null)
				((IXmlSerializable)_style).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
	}
}
