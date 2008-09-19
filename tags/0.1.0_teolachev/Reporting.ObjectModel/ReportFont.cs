using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;


namespace Reporting.ObjectModel
{
	[TypeConverter(typeof(ReportFontConverter))]
    public sealed class ReportFont: IXmlSerializable
	{
		public enum FontStyleType
		{
			Normal,
			Italic
		}

        internal const string XML_REPORT_FONT = "ReportFont";
        internal const string XML_FONT_FAMILY = "ReportFontFamily";
        internal const string XML_FONT_SIZE = "ReportFontSize";
        internal const string XML_FONT_STYLE = "ReportFontStyle";
        internal const string XML_FONT_WEIGHT = "ReportFontWeight";
        internal const string XML_FONT_UNDERLINE = "ReportFontUnderline";

		#region Costants

        private static readonly Unit FONT_SIZE_MIN = new Unit(1, UnitType.Point);
		private static readonly Unit FONT_SIZE_MAX = new Unit(200, UnitType.Point);
        private static string[] _supportedFonts = {"Arial","Arial Black","Comic Sans MS","Courier New","Georgia","Impact","Lucida Console ","Lucida Sans Unicode","Microsoft Sans Serif","Modern","Palatino Linotype","Roman","Tahoma","Times New Roman","Verdana"};

		#endregion

		#region Private Variables

		private string				_fontFamily;
		private Unit				_fontSize;
		private FontStyleType		_fontStyle;
		private ReportFontWeight	_fontWeight;
  

		#endregion

		#region Constructors

		public ReportFont()
        {
            _fontWeight = new ReportFontWeight();
		}

		public ReportFont(FontStyleType fontStyle, string fontFamily, Unit fontSize, ReportFontWeight fontWeight)
        {
            FontStyle = fontStyle;
            FontFamily = fontFamily;
            FontSize = fontSize;
            FontWeight = fontWeight;
        }

		public ReportFont(string fontStyle, string fontFamily, string fontSize, string fontWeight)
			: this((FontStyleType)Enum.Parse(typeof(FontStyleType), fontStyle), fontFamily, new Unit(fontSize), new ReportFontWeight(null))
        {
            FontWeight.Text = fontWeight;
		}
		
		#endregion

		#region Public Properties

		[TypeConverter(typeof(FontFamilyConverter))]
        public string FontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }

        public Unit FontSize
        {
            get { return _fontSize; }
            set
            {
                Unit.ValidateRange("FontSize", value, FONT_SIZE_MIN, FONT_SIZE_MAX);
				_fontSize = value;
            }
        }

		public FontStyleType FontStyle
        {
            get { return _fontStyle; }
            set { _fontStyle = value; }
        }

        [TypeConverter(typeof(ReportFontWeight.FontWeightConverter))]
        public ReportFontWeight FontWeight
        {
            get { return _fontWeight; }
            set { _fontWeight = value; }
        }



        [XmlIgnore, Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if ((string.IsNullOrEmpty(_fontFamily) && _fontSize.IsEmpty))
                {
                    return string.IsNullOrEmpty(_fontWeight.Value);
                }
                return false;
            }
		}

        public static string[] SupportedFonts
        {
            get
            {
                return _supportedFonts;
            }
        }
		#endregion

		#region Public Methods

		public Font ToFont()
		{
			return new Font(_fontFamily, (float)_fontSize.Value, (_fontStyle == FontStyleType.Italic ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular), GraphicsUnit.Point);
		}

		public static Font ToFont(ReportFont reportFont)
		{
			return new Font(reportFont.FontFamily, (float)reportFont.FontSize.Value, (reportFont.FontStyle == FontStyleType.Italic ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular), GraphicsUnit.Point);
		}

		//public static ReportFont FromFont(Font font)
		//{
		//    ReportFont reportFont = new ReportFont();
		//    reportFont.FontFamily = font.FontFamily.ToString();
		//    reportFont.FontSize = Unit.Parse(font.Size.ToString());
		//    reportFont.FontStyle = (font.Style == FontStyle.Italic ? FontStyleType.Italic : FontStyleType.Normal);
		//}

		#endregion

		internal sealed class ReportFontConverter : TypeConverter
        {
			private const char DELIMITER = ',';

			public ReportFontConverter() { }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;

				return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(InstanceDescriptor))
                    return true;

				return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (!(value is string))
                    return base.ConvertFrom(context, culture, value);

				string fontText = ((string)value).Trim();
				if (fontText.Length == 0)
                    return new ReportFont();

				if (culture == null)
                    culture = CultureInfo.CurrentCulture;

				string[] fontTextArray = fontText.Split(DELIMITER);
				if ((fontTextArray.Length <= 0) || (fontTextArray.Length > 4))
					throw new ArgumentException(fontText);

				return new ReportFont(fontTextArray[0].Trim(), (fontTextArray.Length > 1) ? fontTextArray[1].Trim() : null,
					(fontTextArray.Length > 2) ? fontTextArray[2].Trim() : null, 
					(fontTextArray.Length > 3) ? fontTextArray[3].Trim() : null);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == null)
                    throw new ArgumentNullException("destinationType");

				if ((destinationType == typeof(string)) && (value is ReportFont))
                {
                    ReportFont font = (ReportFont) value;
                    if (font.IsEmpty)
                        return string.Empty;

					if (culture == null)
                        culture = CultureInfo.CurrentCulture;

                    string[] fontText = new string[4];
                    int index = 0;

					fontText[index++] = font.FontStyle.ToString();
					fontText[index++] = font.FontFamily;
					fontText[index++] = font.FontSize.ToString();
					fontText[index++] = font.FontWeight.Text;

					return string.Join(DELIMITER.ToString() + " ", fontText);
                }

                if ((destinationType == typeof(InstanceDescriptor)) && (value is ReportFont))
                {
                    ReportFont font = (ReportFont) value;
                    Type[] typeArray = new Type[] { typeof(FontStyle), typeof(string), typeof(Unit), typeof(string) } ;
                    ConstructorInfo ctorInfo = typeof(ReportFont).GetConstructor(typeArray);
					if (ctorInfo != null)
                    {
                        object[] objArray = new object[] { font.FontStyle, font.FontFamily, font.FontSize, font.FontWeight } ;
						return new InstanceDescriptor(ctorInfo, objArray);
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
            {
                 return new ReportFont((FontStyleType)propertyValues["FontStyle"], (string)propertyValues["FontFamily"],
                                      (Unit)propertyValues["FontSize"], new ReportFontWeight(ReportFontWeight.GetValueFromString((string)propertyValues["FontWeight"]), (string)propertyValues["FontWeight"]));//(ReportFontWeight)propertyValues["FontWeight"]);
            }

            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ReportFont), attributes);
                string[] fontArray = new string[]{ "FontStyle", "FontFamily", "FontSize", "FontWeight" } ;
				return properties.Sort(fontArray);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

        }

		internal sealed class FontFamilyConverter : TypeConverter
		{
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				InstalledFontCollection fontCollection = new InstalledFontCollection();
				FontFamily[] fontFamilies = fontCollection.Families;
				string[] fontFamilyNames = new string[fontFamilies.Length];
				for (int i = 0; i < fontFamilies.Length; i++)
				{
					fontFamilyNames[i] = fontFamilies[i].Name;
				}
				return new StandardValuesCollection(fontFamilyNames);
			}
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
        /// Generates an Footer from its RDL representation.
        /// </summary>
        /// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Footer is deserialized</param>
        public void ReadXml(XmlReader reader)
        {

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == XML_REPORT_FONT)
                {
                    break;
                }
                else if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case XML_FONT_FAMILY    : _fontFamily = reader.ReadString(); break;
                        case XML_FONT_SIZE      : _fontSize = new Unit(reader.ReadString(), null, UnitType.Point); break;
                        case XML_FONT_STYLE     : _fontStyle = (FontStyleType)Enum.Parse(typeof(FontStyleType), reader.ReadString()); break;
                        case XML_FONT_WEIGHT    : _fontWeight.Value =  reader.ReadString(); break;
                    }
                }
            }
        }

        /// <summary>
        /// Converts a Footer into its RDL representation .
        /// </summary>
        /// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Footer is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            //--- Header
            writer.WriteStartElement(XML_REPORT_FONT);

            //---XML_FONT_FAMILY
            writer.WriteElementString(XML_FONT_FAMILY, _fontFamily.ToString());

            //---XML_FONT_SIZE
            writer.WriteElementString(XML_FONT_SIZE, _fontSize.Value.ToString());

            //---XML_FONT_STYLE
            writer.WriteElementString(XML_FONT_STYLE, _fontStyle.ToString());

            //---XML_FONT_WEIGHT
            writer.WriteElementString(XML_FONT_WEIGHT, _fontWeight.Value);

            writer.WriteEndElement();
        }

        #endregion IXmlSerializable Members
    }

         internal sealed class FontNameConverter : TypeConverter
        {
            public FontNameConverter() { }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(ReportFont.SupportedFonts);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                // show a combobox
                return true;
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
        }
}

