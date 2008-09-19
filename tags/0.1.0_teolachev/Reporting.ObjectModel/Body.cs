using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// The top level container for all report items and it defines 
	/// the visual elements of the report, how the data is structure/grouped, 
	/// and binds the visual elements to the data sets for the report.
	/// </summary>
	public class Body : IXmlSerializable
	{
		private ReportItemCollection	_reportItems;
		private Size					_height;		
		private int						_columns;
		private Size					_columnSpacing; 
		private Style					_style;

		/// <summary>
		/// Creates a new instance of a Body.
		/// </summary>
		public Body()
		{
			//--- Set the defaults
			_columns = 1;
			_columnSpacing = Size.Parse("0.5in");
		}

		/// <summary>
		/// The region that contains the elements of the report body.
		/// </summary>
		public ReportItemCollection ReportItems
		{
			get 
			{
                
				if(_reportItems == null)
					_reportItems = new ReportItemCollection();

				return _reportItems;
			}

			set {_reportItems = value;}
		}



        /// <summary>
        /// Height of the body.
        /// </summary>
        public Size Height
		{
			get {return _height;}
			set {_height = value;}
		}

		/// <summary>
		/// Number of columns for the report.
		/// </summary>
		[DefaultValue(1)]
		public int Columns
		{
			get {return _columns;}
			set {_columns = value;}
		}

		/// <summary>
		/// Spacing between each column in multi-column output.
		/// </summary>
		[DefaultValue("0.5in")]
		public Size ColumnSpacing
		{
			get {return _columnSpacing;}
			set {_columnSpacing = value;}
		}

		/// <summary>
		/// Default style information for the body.
		/// </summary>
		public Style Style
		{
			get {return _style;}
			set {_style = value;}
		}

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
		/// Generates an Body from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Body is deserialized</param>
		public void ReadXml(System.Xml.XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.BODY)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
				{
					//--- Report Items
					if (reader.Name == Rdl.REPORTITEMS && !reader.IsEmptyElement)
					{
						if (_reportItems == null)
							_reportItems = new ReportItemCollection();

						((IXmlSerializable)_reportItems).ReadXml(reader);
					}

					//--- Height
					if (reader.Name == Rdl.HEIGHT)
						_height = Size.Parse(reader.ReadString());

					//--- Columns
					if (reader.Name == Rdl.COLUMNS)
						_columns = int.Parse(reader.ReadString());

					//--- ColumnSpacing
					if (reader.Name == Rdl.COLUMNSPACING)
						_columnSpacing = Size.Parse(reader.ReadString());

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
		/// Converts a Body into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Body is serialized</param>
		public void WriteXml(System.Xml.XmlWriter writer)
		{
			//--- Body
			writer.WriteStartElement(Rdl.BODY);

			//--- ReportItems
			if (_reportItems != null)
				((IXmlSerializable)_reportItems).WriteXml(writer);

			//--- Height
			writer.WriteElementString(Rdl.HEIGHT, _height.ToString());

			//--- Columns
			if(_columns != 1)
				writer.WriteElementString(Rdl.COLUMNS, _columns.ToString());

			//--- ColumnSpacing
			if (_columnSpacing.ToString() != "0.5in")
				writer.WriteElementString(Rdl.COLUMNSPACING, _columnSpacing.ToString());

			//---Style
			if (_style != null)
				((IXmlSerializable)_style).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
	}
}
