using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines a column in the table.
	/// </summary>
	public class TableColumn : IXmlSerializable
	{
		#region Private Variables

		private Size		_width;
		private Visibility	_visibility ;
		private bool		_fixedHeader;

		#endregion

		/// <summary>
		/// Creates an instance of the TableColumn class.
		/// </summary>
		public TableColumn()
        {
            this.Width = new Size("0.5");
        }
        public TableColumn(double inches)
        {
            this.Width = new Size(inches);
        }

		#region Public Properties

		/// <summary>
		/// Width of the column.
		/// </summary>
		public Size Width
		{
			get {return _width;}
			set {_width = value;}
		}

		/// <summary>
		/// Indicates if the column should be hidden.
		/// </summary>
		public Visibility Visibility
		{
			get {
                if (_visibility==null) _visibility = new Visibility();
                return _visibility;
            }
			set {_visibility = value;}
		}

		public bool FixedHeader
		{
			get {return _fixedHeader;}
			set {_fixedHeader = value;}
		}


		#endregion

	
		#region IXmlSerializable Members

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema  GetSchema()
		{
 			return null;
		}

		/// <summary>
		/// Generates an TableColumn from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableColumn is deserialized</param>
		public void  ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLECOLUMN)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Width
					if (reader.Name == Rdl.WIDTH)
						_width = Size.Parse(reader.ReadString());

					//--- Visibility
					if (reader.Name == Rdl.VISIBILITY)
					{
						if (_visibility == null)
							_visibility = new Visibility();

						((IXmlSerializable)_visibility).ReadXml(reader);
					}

					//--- FixedHeader
					if (reader.Name == Rdl.FIXEDHEADER)
						_fixedHeader = bool.Parse(reader.ReadString());
				}
			}		
		}

		/// <summary>
		/// Converts a TableColumn into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableColumn is serialized.</param>
		public void  WriteXml(XmlWriter writer)
		{
			//--- TableColumn
			writer.WriteStartElement(Rdl.TABLECOLUMN);

			//--- Width
			writer.WriteElementString(Rdl.WIDTH, _width.ToString());

			//--- Visibility
			if (_visibility != null)
				((IXmlSerializable)_visibility).WriteXml(writer);

			//--- FixedHeader
			writer.WriteElementString(Rdl.FIXEDHEADER, _fixedHeader.ToString().ToLower());

			writer.WriteEndElement();		
		}

		#endregion
	}
}
