using System;
using System.Xml;
using System.Drawing;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{

	public class Textbox : ReportItem
	{
        #region Constants
        public const string PREFIX = "t";
        internal const string ENTITY_NAME = "Entity";
        internal const string TEXTBOX_HEIGHT = "0.25";
 
        #endregion  

        private Expression				_value;
		private bool					_canGrow;
		private bool					_canShrink;
		private string					_hideDuplicates;
		private DataElementStyle		_dataElementStyle;

		public Textbox()
        {
            this.Height = new Size(TEXTBOX_HEIGHT);
            this._canGrow = false; // expand the textbox vertically by default
        }
        public Textbox(string name):this()
        {
            this._name = name;
        }

		/// <summary>
		/// An expression, the value of which is displayed in the Textbox.
		/// </summary>
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
		/// Indicates the Textbox size can increase to accomodate the contents.
		/// </summary>
		public bool CanGrow
		{
			get {return _canGrow;}
			set {_canGrow = value;}
		}

		/// <summary>
		/// Indicates the Textbox size can decrease to match the contents.
		/// </summary>
		public bool CanShrink
		{
			get {return _canShrink;}
			set {_canShrink = value;}
		}

		/// <summary>
		/// Indicates the item should be hidden when the value of the expression
		/// associated with the report item is the same as the preceding instance.
		/// </summary>
		public string HideDuplicates
		{
			get {return _hideDuplicates;}
			set {_hideDuplicates = value;}
		}


		/// <summary>
		/// Indicates whether textbox value should render as an element or attribute
		/// </summary>
		public DataElementStyle DataElementStyle
		{
			get {return _dataElementStyle;}
			set {_dataElementStyle = value;}
		}

		#region Hidden Properties

		/// <summary>
		/// Does not apply to a Textbox.
		/// </summary>
		private new string LinkToChild
		{
			get {return base.LinkToChild;}
			set{base.LinkToChild = value;}
		}

		#endregion

		#region Protected Methods

		protected override string GetRootElement()
		{
			return Rdl.TEXTBOX;
		}

		protected override void WriteRDL(XmlWriter writer)
		{
			//--- Value
			if (_value != null)
				writer.WriteElementString(Rdl.VALUE, _value.Value!=null?_value.Value.ToString():String.Empty);
			else
				writer.WriteElementString(Rdl.VALUE, string.Empty);

			//--- CanGrow
			if(_canGrow)
				writer.WriteElementString(Rdl.CANGROW, _canGrow.ToString().ToLower());

			//--- CanShrink
			if(_canShrink)
				writer.WriteElementString(Rdl.CANSHRINK, _canShrink.ToString().ToLower());

			//--- HideDuplicates
			if(_hideDuplicates != null && _hideDuplicates != string.Empty)
				writer.WriteElementString(Rdl.HIDEDUPLICATES, _hideDuplicates);



			//--- DataElementStyle
			if(_dataElementStyle != DataElementStyle.Auto)
				writer.WriteElementString(Rdl.DATAELEMENTSTYLE, _dataElementStyle.ToString());
		}

		protected override void ReadRDL(XmlReader reader)
		{
			//--- Value
			if (reader.Name == Rdl.VALUE)
			{
				if (_value == null)
					_value = new Expression();

				_value.Value = reader.ReadString();
			}

			//--- CanGrow
			if (reader.Name == Rdl.CANGROW)
				_canGrow = bool.Parse(reader.ReadString());

			//--- CanShrink
			if (reader.Name == Rdl.CANSHRINK)
				_canShrink = bool.Parse(reader.ReadString());

			//--- HideDuplicates
			if(reader.Name == Rdl.HIDEDUPLICATES)
				_hideDuplicates = reader.ReadString();


			//--- DataElementStyle
			if (reader.Name == Rdl.DATAELEMENTSTYLE)
				_dataElementStyle = (DataElementStyle)Enum.Parse(typeof(DataElementStyle), reader.ReadString());
		}

		#endregion

        #region Report Generation
        /// <summary>
        /// Returns a standard textbox report item
        /// </summary>
        /// <param name="name">The name of the textbox</param>
        /// <param name="field">The binding dataset field or null if not bound</param>
        /// <returns></returns>
        internal static Textbox GetStandardTextBox(string name, string value)
        {
            Textbox t = new Textbox();
            t.Name = name;

            if (value != null)
            {
                t.Value = new Expression(value);
            }

            t.Style = new Style();
            t.Style.PaddingLeft = t.Style.PaddingRight =
            t.Style.PaddingTop = t.Style.PaddingBottom = new Expression("2pt");
            t.Style.BorderWidth.Default = new Expression("1pt");
            t.CanGrow = false;

            return t;
        }

        public static Textbox GetStandardTextBox()
        {
            return GetStandardTextBox(null, null);
        }

   
        /// <summary>
        /// Used in a freeform (list report)
        /// </summary>
        /// <param name="name">The textbox name</param>
        /// <param name="value">The textbox value</param>
        /// <returns>Textbox report item</returns>
        internal static Textbox GetGroupByTextBox(string name, string value)
        {
            Textbox t = Textbox.GetStandardTextBox(name, value);
            t.Style.FontFamily.Value = "Verdana";
            t.Style.FontSize.Value = "12pt";
            t.Style.FontWeight.Value = "Bold";
            return t;
        }
        internal void SetLineNumberTextBox()
        {
            
            this.Name = String.Format("{0}{1}", Textbox.PREFIX, "LineNumber");
            this.Value = new Expression("=RowNumber(Nothing)");
            this.Style.TextAlign = new Expression("Right");
            this.Style.FontSize = new Expression("8pt");


        }
  
        private string GetBorderWidth (string style, string width)
        {
            int result;
            

            if (int.TryParse(width, out result))
            {
                int w = result < 2 && style.ToLower() == "double"? 2: result;
                return String.Format("{0}pt", w);
            }
            else
                return width;
        }

        public ReportFont ReportFont
        {
            get
            {
                return new ReportFont(this.Style.FontStyle.ToString(), this.Style.FontFamily.ToString(), 
                    this.Style.FontSize.ToString(), this.Style.FontWeight.ToString());
            }
        }


        
        #endregion
    }
}
