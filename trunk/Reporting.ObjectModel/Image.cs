using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	public class Image : ReportItem
	{
        public const string PREFIX = "img";
		#region Private Variables

		private Source			_source;
		private Expression		_value;
		private Expression		_MIMEType;
		private Sizing			_sizing;
		
		#endregion

		/// <summary>
		/// Creates an instance of an Image class.
		/// </summary>
		public Image(){}

		#region Public Properties

		/// <summary>
		/// Identifies the source of the image.
		/// </summary>
		public Source Source
		{
			get {return _source;}
			set {_source = value;}
		}

		public Expression Value
		{
			get 
            {
                if (_value == null)
                    _value = new Expression();

                return _value;
            }
			set {_value = value;}
		}

		/// <summary>
		/// The MIMEType for the image.
		/// </summary>
		public Expression MIMEType
		{
            get
            {
                if (_MIMEType == null)
                    _MIMEType = new Expression();

                return _MIMEType;
            }
            set { _MIMEType = value; }
		}

		/// <summary>
		/// Defines the bahavior if the image does not fit within the specified size.
		/// </summary>
		public Sizing Sizing
		{
			get {return _sizing;}
			set {_sizing = value;}
		}

		#endregion

		#region Hidden Properties

		private new string LinkToChild
		{
			get {return base.LinkToChild;}
			set{base.LinkToChild = value;}
		}

		#endregion

		#region Protected Methods

		protected override string GetRootElement()
		{
			return Rdl.IMAGE;
		}

		protected override void WriteRDL(XmlWriter writer)
		{
			//--- Source
			writer.WriteElementString(Rdl.SOURCE, _source.ToString());

			//--- Value
			if(_value != null)
				writer.WriteElementString(Rdl.VALUE, _value.Value.ToString());
			
			//--- MIMEType
			if(_MIMEType != null)
				writer.WriteElementString(Rdl.MIMETYPE, _MIMEType.Value.ToString());

			//--- Sizing
			writer.WriteElementString(Rdl.SIZING, _sizing.ToString());
		}

		protected override void ReadRDL(XmlReader reader)
		{
			//--- Source
			if (reader.Name == Rdl.SOURCE)
				_source = (Source)Enum.Parse(typeof(Source), reader.ReadString());

			//--- Value
			if (reader.Name == Rdl.VALUE)
			{
				if (_value == null)
					_value = new Expression();

				_value.Value = reader.ReadString();
			}

			//--- MIMEType
			if (reader.Name == Rdl.MIMETYPE)
			{
				if (_MIMEType == null)
					_MIMEType = new Expression();

				_MIMEType.Value = reader.ReadString();
			}

			//--- Sizing
			if (reader.Name == Rdl.SIZING)
				_sizing = (Sizing)Enum.Parse(typeof(Sizing), reader.ReadString());
		}

		#endregion
	}
}
