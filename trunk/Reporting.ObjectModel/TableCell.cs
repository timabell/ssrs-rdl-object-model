using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the contents of a cell in a table data region.
	/// </summary>
	public class TableCell : IXmlSerializable
	{
		#region Private Variables

		private ReportItemCollection	_reportItems;
		private int						_colSpan;

		#endregion

		/// <summary>
		/// Creates an instance of a TableCell class.
		/// </summary>
		public TableCell(){}

        // Generates an empty textbox in the cell
        public TableCell(string textboxName, TableColumn tc) 
        {
            Textbox t = Textbox.GetStandardTextBox(textboxName, null);
            t.Width = tc.Width;
            this.ReportItems.Add(t);
        }
        // Generates an empty textbox in the cell
        //public TableCell(string textboxName, string textboxValue, TableColumn tc)
        //{
        //    Textbox t = Textbox.GetStandardTextBox(textboxName, textboxValue);
        //    t.Width = tc.Width;
        //    this.ReportItems.Add(t);
        //}

        public TableCell(string textboxName, Size s)
        {
            Textbox t = Textbox.GetStandardTextBox(textboxName, null);
            t.Width = s;
            this.ReportItems.Add(t);
        }


        // Generates an empty textbox in the cell
        public TableCell(Textbox t)
        {
            this.ReportItems.Add(t);

        }

		#region Public Properties

		/// <summary>
		/// An element of the report layout.
		/// </summary>
		public ReportItemCollection ReportItems
		{
			get 
			{
				if(_reportItems == null)
					_reportItems = new ReportItemCollection();

				return _reportItems;
			}

		}

		/// <summary>
		/// Indicates the number of columns this cell spans.
		/// </summary>
		public int ColSpan
		{
			get {return _colSpan;}
			set {_colSpan = value;}
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
		/// Generates an TableCell from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableCell is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLECELL)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- ReportItems
					if (reader.Name == Rdl.REPORTITEMS)
					{
						if (_reportItems == null)
							_reportItems = new ReportItemCollection();

						((IXmlSerializable)_reportItems).ReadXml(reader);
					}

					//--- ColSpan
					if (reader.Name == Rdl.COLSPAN)
						_colSpan = int.Parse(reader.ReadString());
				}
			}
		}

		/// <summary>
		/// Converts a TableCell into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableCell is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- TableCell
			writer.WriteStartElement(Rdl.TABLECELL);

			//--- ReportItems
			if (_reportItems != null)
				((IXmlSerializable)_reportItems).WriteXml(writer);

			//--- ColSpan
			if(_colSpan > 0)
				writer.WriteElementString(Rdl.COLSPAN, _colSpan.ToString());

			writer.WriteEndElement();
		}

		#endregion
}
}
