using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Represents a Rectangle report item on a report.
	/// </summary>
	public class Rectangle : ReportItem
	{
        private const string PREFERRED_HEIGHT = "rd:PreferredHeight";

        internal static string PREFIX = "rec";
		private bool _pageBreakAtStart;
		private bool _pageBreakAtEnd;
        private Size _preferredHeight = new Size(0d);
		private ReportItemCollection _reportItems;

		/// <summary>
		/// Creates an instance of the Rectangle class.
		/// </summary>
		public Rectangle(){}
        public Rectangle(string name): this() 
        {
            base.Name = name;
        }

		/// <summary>
		/// Indicates the report should page break at the start of the rectangle.
		/// </summary>
		public bool PageBreakAtStart
		{
			get {return _pageBreakAtStart;}
			set {_pageBreakAtStart = value;}
		}

		/// <summary>
		/// Indicates the report should page break at the end of the rectangle.
		/// </summary>
		public bool PageBreakAtEnd
		{
			get {return _pageBreakAtEnd;}
			set {_pageBreakAtEnd = value;}
		}

        /// <summary>
        /// Set the preferred height of the rectangle
        /// Used by the WYSWYG designer
        /// </summary>
        public Size PreferredHeight
        {
            get
            {
                return _preferredHeight;
            }
            set
            {
                _preferredHeight = value;
            }
        }

        /// <summary>
        /// Set the rectangle section border
        /// </summary>
        public void SetSectionBorder(bool top)
        {
            if (top)
            {
                this.Style.BorderStyle.Top.Value = "Solid";
                this.Style.BorderColor.Top.Value = "Black";
                this.Style.BorderWidth.Top.Value = "1pt";
            }
            else
            {
                this.Style.BorderStyle.Bottom.Value = "Solid";
                this.Style.BorderColor.Bottom.Value = "Black";
                this.Style.BorderWidth.Bottom.Value = "1pt";
            }

        }

        public void ClearSectionBorder(bool top)
        {
            if (top)
            {
                this.Style.BorderStyle.Top.Value = "";
            }
            else
            {
                this.Style.BorderStyle.Bottom.Value = "";
            }

        }
		/// <summary>
		/// Report items contained within the bounds of the rectangle.
		/// </summary>
		public ReportItemCollection ReportItems
		{
			get 
			{
				if(_reportItems == null)
					_reportItems = new ReportItemCollection();

				return _reportItems;
			}
            set { _reportItems = value; }
		}

        

		#region Methods




		#endregion

		#region Protected Methods

		protected override string GetRootElement()
		{
			return Rdl.RECTANGLE;
		}

        internal override bool ShouldSerialize
        {
            get
            {
                if (this.Visibility.Hidden.Value.ToString() == "true")
                    return false;
                else
                    return true;
            }
        }
		protected override void WriteRDL(XmlWriter writer)
		{
            //if (!ShouldSerialize) return;
			//--- PageBreakAtStart
			writer.WriteElementString(Rdl.PAGEBREAKATSTART, _pageBreakAtStart.ToString().ToLower());

			//--- PageBreakAtEnd
			writer.WriteElementString(Rdl.PAGEBREAKATEND, _pageBreakAtEnd.ToString().ToLower());

            //--- Prefered Height
            if (_preferredHeight.Unit.Value > 0)
                writer.WriteElementString(PREFERRED_HEIGHT, _preferredHeight.ToString());

			//--- ReportItems
			if (_reportItems != null && _reportItems.Count > 0)
				((IXmlSerializable)_reportItems).WriteXml(writer);
		}

		protected override void ReadRDL(XmlReader reader)
		{
            switch (reader.Name)
            {
                case Rdl.PAGEBREAKATSTART:  _pageBreakAtStart = bool.Parse(reader.ReadString()); break;
                case Rdl.PAGEBREAKATEND:    _pageBreakAtEnd = bool.Parse(reader.ReadString()); break;
                case PREFERRED_HEIGHT:      _preferredHeight = Size.Parse(reader.ReadString()); break;
                                            
                case Rdl.REPORTITEMS:       if (!reader.IsEmptyElement)
                                            {
                                                if (_reportItems == null) _reportItems = new ReportItemCollection();
                                                ((IXmlSerializable)_reportItems).ReadXml(reader);
                                            }
                                            break;
            }

        }

		#endregion
	}
}
