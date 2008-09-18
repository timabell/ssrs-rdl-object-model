using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines a row of cells in a table data region.
	/// </summary>
	public class TableRow : IXmlSerializable
    {
        #region Constants
        internal const string DEFAULT_HEIGHT = "0.25";
        #endregion
        #region Private Variables

        private TableCellCollection	_tableCells;
		private Size				_height;
		private Visibility			_visibility;

		#endregion

        #region Constructors
        /// <summary>
		/// Creates and instance of the TableRow class.
		/// </summary>
		public TableRow()
        {
            this.Height = new Size(Textbox.TEXTBOX_HEIGHT); // default height
        }

        public TableRow(int cellCount)
            : this(cellCount, new Size(Textbox.TEXTBOX_HEIGHT), null) { }

        public TableRow(int cellCount, Size height)
            : this(cellCount, height, null)
        {

        }

        public TableRow(int cellCount, Size height, string namePrefix)
            : this()
        {
            TableCell c = null;
            Textbox t = null;

            this.Height = height;

            for (int i = 0; i < cellCount; i++)
            {
                c = new TableCell();
                t = Textbox.GetStandardTextBox();
                if (namePrefix!=null) t.Name = String.Format("{0}_{1}", namePrefix, i); 
                t.Height = height;
                c.ReportItems.Add(t);
                this.TableCells.Add(c);
            }
        }
        #endregion Constructors

        #region Public Properties

        /// <summary>
		/// Contents of the row.
		/// </summary>
		public TableCellCollection TableCells
		{
			get 
			{
				if(_tableCells == null)
					_tableCells = new TableCellCollection();

				return _tableCells;
			}
		}

		/// <summary>
		/// Height of the row.
		/// </summary>
		public Size Height
		{
			get {return _height;}
			set {
                _height = value;
            }
		}

		/// <summary>
		/// Inidicates if the row should be hidden.
		/// </summary>
		public Visibility Visibility
		{
			get 
            {
                if (_visibility == null) _visibility = new Visibility();
                return _visibility;
            }
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
		/// Generates an TableRow from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableRow is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLEROW)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- TableCells
					if (reader.Name == Rdl.TABLECELLS)
					{
						if (_tableCells == null)
							_tableCells = new TableCellCollection();

						((IXmlSerializable)_tableCells).ReadXml(reader);
					}

					//--- Height
					if (reader.Name == Rdl.HEIGHT)
						_height = Size.Parse(reader.ReadString());


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
		/// Converts a TableRow into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableRow is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- TableRow
			writer.WriteStartElement(Rdl.TABLEROW);

			//--- TableCells
			if (_tableCells != null)
				((IXmlSerializable)_tableCells).WriteXml(writer);

			//--- Height
			writer.WriteElementString(Rdl.HEIGHT, _height.ToString());

			//--- Visibility
			if (_visibility != null)
				((IXmlSerializable)_visibility).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
	}
}
