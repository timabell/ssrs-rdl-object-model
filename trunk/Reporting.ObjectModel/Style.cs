using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Drawing;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// The Style element contains information about the style of a report item.  Where possible, 
	/// the style property names and values match standard HTML/CSS properties.
	/// </summary>
	public class Style : IXmlSerializable
	{
		#region Private Variables

		private BorderColor		_borderColor;
		private BorderStyle		_borderStyle;
		private BorderWidth		_borderWidth;
		private Expression<Color> _backgroundColor;
		private Expression		_backgroundGradientType;
		private Expression<Color> _backgroundGradientEndColor;
		private Expression		_fontStyle;
		private Expression		_fontFamily;
		private Expression		_fontSize;
		private Expression		_fontWeight;
		private Expression		_format;
		private Expression		_textDecoration;
		private Expression		_textAlign;
		private Expression		_verticalAlign;
		private Expression<Color> _color;
		private Expression		_paddingLeft;
		private Expression		_paddingRight;
		private Expression		_paddingTop;
		private Expression		_paddingBottom;
		private Expression		_lineHeight;
		private Expression		_direction;
		private Expression		_writingMode;
		private Expression		_language;
		private Expression		_unicodeBiDi;
		private Expression		_calendar;
		private Expression		_numeralLanguage;
		private Expression		_numeralVariant;

		#endregion

		/// <summary>
		/// Creates a new instance of the Style class.
		/// </summary>
		public Style(){}

		/// <summary>
		/// Colo of the border.
		/// </summary>
		public BorderColor BorderColor
		{
			get 
			{ 
				if(_borderColor == null)
					_borderColor = new BorderColor();

				return _borderColor; 
			}
			set { _borderColor = value; }
		}

		/// <summary>
		/// Style of the border.
		/// </summary>
		public BorderStyle BorderStyle
		{
			get
			{
				if (_borderStyle == null)
					_borderStyle = new BorderStyle();

				return _borderStyle;
			}
			set { _borderStyle = value; }
		}

		/// <summary>
		/// Width of the border.
		/// </summary>
		public BorderWidth BorderWidth
		{
			get
			{
				if (_borderWidth == null)
					_borderWidth = new BorderWidth();

				return _borderWidth;
			}
			set { _borderWidth = value; }
		}

		/// <summary>
		/// Color of the background.
		/// </summary>
		public Expression<Color> BackgroundColor
		{
			get
			{
				if (_backgroundColor == null)
					_backgroundColor = new Expression<Color>();

				return _backgroundColor;
			}
			set { _backgroundColor = value; }
		}

		/// <summary>
		/// The type of background gradient.
		/// </summary>
		public Expression BackgroundGradientType
		{
			get
			{
				if (_backgroundGradientType == null)
					_backgroundGradientType = new Expression();

				return _backgroundGradientType;
			}
			set { _backgroundGradientType = value; }
		}

		/// <summary>
		/// End color for the background gradient.
		/// </summary>
		public Expression<Color> BackgroundGradientEndColor
		{
			get
			{
				if (_backgroundGradientEndColor == null)
					_backgroundGradientEndColor = new Expression<Color>();

				return _backgroundGradientEndColor;
			}
			set { _backgroundGradientEndColor = value; }
		}


		/// <summary>
		/// Font style.
		/// </summary>
		public Expression FontStyle
		{
			get
			{
				if (_fontStyle == null)
					_fontStyle = new Expression();

				return _fontStyle;
			}
			set { _fontStyle = value; }
		}

		/// <summary>
		/// Name of the font family.
		/// </summary>
		public Expression FontFamily
		{
			get
			{
				if (_fontFamily == null)
					_fontFamily = new Expression();

				return _fontFamily;
			}
			set { _fontFamily = value; }
		}

		/// <summary>
		/// Point size of the font.
		/// </summary>
		public Expression FontSize
		{
			get
			{
				if (_fontSize == null)
					_fontSize = new Expression();

				return _fontSize;
			}
			set { _fontSize = value; }
		}

		/// <summary>
		/// Thickness of the font.
		/// </summary>
		public Expression FontWeight
		{
			get
			{
				if (_fontWeight == null)
					_fontWeight = new Expression();

				return _fontWeight;
			}
			set { _fontWeight = value; }
		}

		/// <summary>
		/// .NET Framework formatting string.
		/// </summary>
		public Expression Format
		{
			get
			{
				if (_format == null)
					_format = new Expression();

				return _format;
			}
			set { _format = value; }
		}

		/// <summary>
		/// Special text formatting.
		/// </summary>
		public Expression TextDecoration
		{
			get
			{
				if (_textDecoration == null)
					_textDecoration = new Expression();

				return _textDecoration;
			}
			set { _textDecoration = value; }
		}

		/// <summary>
		/// Horizontal alignment of the text.
		/// </summary>
		public Expression TextAlign
		{
			get
			{
				if (_textAlign == null)
					_textAlign = new Expression();

				return _textAlign;
			}
			set { _textAlign = value; }
		}

		/// <summary>
		/// Vertical alignment of the text.
		/// </summary>
		public Expression VerticalAlign
		{
			get
			{
				if (_verticalAlign == null)
					_verticalAlign = new Expression();

				return _verticalAlign;
			}
			set { _verticalAlign = value; }
		}

		/// <summary>
		/// The foreground color.
		/// </summary>
		public Expression<Color> Color
		{
			get
			{
				if (_color == null)
					_color = new Expression<Color>();

				return _color;
			}
			set { _color = value; }
		}

		/// <summary>
		/// Padding between the left edge of the
		/// report item and its contents.
		/// </summary>
		public Expression PaddingLeft
		{
			get
			{
				if (_paddingLeft == null)
					_paddingLeft = new Expression();

				return _paddingLeft;
			}
			set { _paddingLeft = value; }
		}

		/// <summary>
		/// Padding between the right edge of the
		/// report item and its contents.
		/// </summary>
		public Expression PaddingRight
		{
			get
			{
				if (_paddingRight == null)
					_paddingRight = new Expression();

				return _paddingRight;
			}
			set { _paddingRight = value; }
		}

		/// <summary>
		/// Padding between the top edge of the
		/// report item and its contents.
		/// </summary>
		public Expression PaddingTop
		{
			get
			{
				if (_paddingTop == null)
					_paddingTop = new Expression();

				return _paddingTop;
			}
			set { _paddingTop = value; }
		}

		/// <summary>
		/// Padding between the top edge of the
		/// report item and its contents.
		/// </summary>
		public Expression PaddingBottom
		{
			get
			{
				if (_paddingBottom == null)
					_paddingBottom = new Expression();

				return _paddingBottom;
			}
			set { _paddingBottom = value; }
		}

		/// <summary>
		/// Height of a line of text.
		/// </summary>
		public Expression LineHeight
		{
			get
			{
				if (_lineHeight == null)
					_lineHeight = new Expression();

				return _lineHeight;
			}
			set { _lineHeight = value; }
		}

		/// <summary>
		/// Indicates whether text is written left-toright
		/// or right-to-left.
		/// </summary>
		public Expression Direction
		{
			get
			{
				if (_direction == null)
					_direction = new Expression();

				return _direction;
			}
			set { _direction = value; }
		}

		/// <summary>
		/// Indicates whether text is written
		/// horizontally or vertically.
		/// </summary>
		public Expression WritingMode
		{
			get
			{
				if (_writingMode == null)
					_writingMode = new Expression();

				return _writingMode;
			}
			set { _writingMode = value; }
		}

		/// <summary>
		/// The primary language of the text.
		/// </summary>
		public Expression Language
		{
			get
			{
				if (_language == null)
					_language = new Expression();

				return _language;
			}
			set { _language = value; }
		}

		/// <summary>
		/// Indicates the level of embedding with
		/// respect to the Bi-directional algorithm.
		/// </summary>
		public Expression UnicodeBiDi
		{
			get
			{
				if (_unicodeBiDi == null)
					_unicodeBiDi = new Expression();

				return _unicodeBiDi;
			}
			set { _unicodeBiDi = value; }
		}

		/// <summary>
		/// Indicates the calendar to use for
		/// formatting dates.
		/// </summary>
		public Expression Calendar
		{
			get
			{
				if (_calendar == null)
					_calendar = new Expression();

				return _calendar;
			}
			set { _calendar = value; }
		}

		/// <summary>
		/// The digit format to use as described by its
		/// primary language.
		/// </summary>
		public Expression NumeralLanguage
		{
			get
			{
				if (_numeralLanguage == null)
					_numeralLanguage = new Expression();

				return _numeralLanguage;
			}
			set { _numeralLanguage = value; }
		}

		/// <summary>
		/// The variant of the digit format to use.
		/// </summary>
		public Expression NumeralVariant
		{
			get
			{
				if (_numeralVariant == null)
					_numeralVariant = new Expression();

				return _numeralVariant;
			}
			set { _numeralVariant = value; }
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
		/// Generates an Style from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Style is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.STYLE)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- BorderColor
					if (reader.Name == Rdl.BORDERCOLOR && !reader.IsEmptyElement)
					{
						if (_borderColor == null)
							_borderColor = new BorderColor();

						((IXmlSerializable)_borderColor).ReadXml(reader);
					}

					//--- BorderStyle
					if (reader.Name == Rdl.BORDERSTYLE && !reader.IsEmptyElement)
					{
						if (_borderStyle == null)
							_borderStyle = new BorderStyle();

						((IXmlSerializable)_borderStyle).ReadXml(reader);
					}

					//--- BorderWidth
					if (reader.Name == Rdl.BORDERWIDTH && !reader.IsEmptyElement)
					{
						if (_borderWidth == null)
							_borderWidth = new BorderWidth();

						((IXmlSerializable)_borderWidth).ReadXml(reader);
					}

					//--- BackgroundColor
					if (reader.Name == Rdl.BACKGROUNDCOLOR && !reader.IsEmptyElement)
					{
						if (_backgroundColor == null)
							_backgroundColor = new Expression<Color>();

						_backgroundColor.Value = reader.ReadString();
					}

					//--- BackgroundGradientType
					if (reader.Name == Rdl.BACKGROUNDGRADIENTTYPE && !reader.IsEmptyElement)
					{
						if (_backgroundGradientType == null)
							_backgroundGradientType = new Expression();

						_backgroundGradientType.Value = reader.ReadString();
					}

					//--- BackgroundGradientEndColor
					if (reader.Name == Rdl.BACKGROUNDGRADIENTENDCOLOR && !reader.IsEmptyElement)
					{
						if (_backgroundGradientEndColor == null)
							_backgroundGradientEndColor = new Expression<Color>();

						_backgroundGradientEndColor.Value = reader.ReadString();
					}


					//--- FontStyle
					if (reader.Name == Rdl.FONTSTYLE && !reader.IsEmptyElement)
					{
						if (_fontStyle == null)
							_fontStyle = new Expression();

						_fontStyle.Value = reader.ReadString();
					}

					//--- FontFamily
					if (reader.Name == Rdl.FONTFAMILY && !reader.IsEmptyElement)
					{
						if (_fontFamily == null)
							_fontFamily = new Expression();

						_fontFamily.Value = reader.ReadString();
					}

					//--- FontSize
					if (reader.Name == Rdl.FONTSIZE && !reader.IsEmptyElement)
					{
						if (_fontSize == null)
							_fontSize = new Expression();

						_fontSize.Value = reader.ReadString();
					}

					//--- FontWeight
					if (reader.Name == Rdl.FONTWEIGHT && !reader.IsEmptyElement)
					{
						if (_fontWeight == null)
							_fontWeight = new Expression();

						_fontWeight.Value = reader.ReadString();
					}

					//--- Format
					if (reader.Name == Rdl.FORMAT && !reader.IsEmptyElement)
					{
						if (_format == null)
							_format = new Expression();

						_format.Value = reader.ReadString();
					}

					//--- TextDecoration
					if (reader.Name == Rdl.TEXTDECORATION && !reader.IsEmptyElement)
					{
						if (_textDecoration == null)
							_textDecoration = new Expression();

						_textDecoration.Value = reader.ReadString();
					}

					//--- TextAlign
					if (reader.Name == Rdl.TEXTALIGN && !reader.IsEmptyElement)
					{
						if (_textAlign == null)
							_textAlign = new Expression();

						_textAlign.Value = reader.ReadString();
					}

					//--- VerticalAlign
					if (reader.Name == Rdl.VERTICALALIGN && !reader.IsEmptyElement)
					{
						if (_verticalAlign == null)
							_verticalAlign = new Expression();

						_verticalAlign.Value = reader.ReadString();
					}

					//--- Color
					if (reader.Name == Rdl.COLOR && !reader.IsEmptyElement)
					{
						if (_color == null)
							_color = new Expression<Color>();

						_color.Value = reader.ReadString();
					}

					//--- PaddingLeft
					if (reader.Name == Rdl.PADDINGLEFT && !reader.IsEmptyElement)
					{
						if (_paddingLeft == null)
							_paddingLeft = new Expression();

						_paddingLeft.Value = reader.ReadString();
					}

					//--- PaddingRight
					if (reader.Name == Rdl.PADDINGRIGHT && !reader.IsEmptyElement)
					{
						if (_paddingRight == null)
							_paddingRight = new Expression();

						_paddingRight.Value = reader.ReadString();
					}

					//--- PaddingTop
					if (reader.Name == Rdl.PADDINGTOP && !reader.IsEmptyElement)
					{
						if (_paddingTop == null)
							_paddingTop = new Expression();

						_paddingTop.Value = reader.ReadString();
					}

					//--- PaddingBottom
					if (reader.Name == Rdl.PADDINGBOTTOM && !reader.IsEmptyElement)
					{
						if (_paddingBottom == null)
							_paddingBottom = new Expression();

						_paddingBottom.Value = reader.ReadString();
					}

					//--- LineHeight
					if (reader.Name == Rdl.LINEHEIGHT && !reader.IsEmptyElement)
					{
						if (_lineHeight == null)
							_lineHeight = new Expression();

						_lineHeight.Value = reader.ReadString();
					}

					//--- Direction
					if (reader.Name == Rdl.DIRECTION && !reader.IsEmptyElement)
					{
						if (_direction == null)
							_direction = new Expression();

						_direction.Value = reader.ReadString();
					}

					//--- WritingMode
					if (reader.Name == Rdl.WRITINGMODE && !reader.IsEmptyElement)
					{
						if (_writingMode == null)
							_writingMode = new Expression();

						_writingMode.Value = reader.ReadString();
					}

					//--- Language
					if (reader.Name == Rdl.LANGUAGE && !reader.IsEmptyElement)
					{
						if (_language == null)
							_language = new Expression();

						_language.Value = reader.ReadString();
					}


					//--- UnicodeBiDi
					if (reader.Name == Rdl.UNICODEBIDI && !reader.IsEmptyElement)
					{
						if (_unicodeBiDi == null)
							_unicodeBiDi = new Expression();

						_unicodeBiDi.Value = reader.ReadString();
					}

					//--- Calendar
					if (reader.Name == Rdl.CALENDAR && !reader.IsEmptyElement)
					{
						if (_calendar == null)
							_calendar = new Expression();

						_calendar.Value = reader.ReadString();
					}

					//--- NumeralLanguage
					if (reader.Name == Rdl.NUMERALLANGUAGE && !reader.IsEmptyElement)
					{
						if (_numeralLanguage == null)
							_numeralLanguage = new Expression();

						_numeralLanguage.Value = reader.ReadString();
					}

					//--- NumeralVariant
					if (reader.Name == Rdl.NUMERALVARIANT && !reader.IsEmptyElement)
					{
						if (_numeralVariant == null)
							_numeralVariant = new Expression();

						_numeralVariant.Value = reader.ReadString();
					}
				}
			}
		}

		/// <summary>
		/// Converts a Style into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Style is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Style
			writer.WriteStartElement(Rdl.STYLE);

			//--- BoderColor
			if (_borderColor != null)
				((IXmlSerializable)_borderColor).WriteXml(writer);

			//---BorderStyle
			if (_borderStyle != null)
				((IXmlSerializable)_borderStyle).WriteXml(writer);

			//---BorderWidth
			if (_borderWidth != null)
				((IXmlSerializable)_borderWidth).WriteXml(writer);

			//--- BackgroundColor
			if (_backgroundColor != null && !string.IsNullOrEmpty(_backgroundColor.Value.ToString()) && _backgroundColor.Value.ToString()!="Transparent")
				writer.WriteElementString(Rdl.BACKGROUNDCOLOR, _backgroundColor.ToString());

			//--- BackgroundGradientType
			if (_backgroundGradientType != null && !string.IsNullOrEmpty(_backgroundGradientType.Value.ToString()))
				writer.WriteElementString(Rdl.BACKGROUNDGRADIENTTYPE, _backgroundGradientType.Value.ToString());

			//--- BackgroundGradientEndColor
			if (_backgroundGradientEndColor != null && !string.IsNullOrEmpty(_backgroundGradientEndColor.Value.ToString()))
				writer.WriteElementString(Rdl.BACKGROUNDGRADIENTENDCOLOR, _backgroundGradientEndColor.ToString());

			//--- FontStyle
			if (_fontStyle != null && !string.IsNullOrEmpty(_fontStyle.Value.ToString()))
				writer.WriteElementString(Rdl.FONTSTYLE, _fontStyle.Value.ToString());

			//--- FontFamily
			if (_fontFamily != null && !string.IsNullOrEmpty(_fontFamily.Value.ToString()))
				writer.WriteElementString(Rdl.FONTFAMILY, _fontFamily.Value.ToString());

			//--- FontSize
			if (_fontSize != null && !string.IsNullOrEmpty(_fontSize.Value.ToString()))
			{
				int result;
				writer.WriteElementString(Rdl.FONTSIZE, int.TryParse(_fontSize.Value.ToString(),
					out result) ? String.Format("{0}pt", result) : _fontSize.Value.ToString());
			}

			//--- FontWeight
			if (_fontWeight != null && !string.IsNullOrEmpty(_fontWeight.Value.ToString()))
				writer.WriteElementString(Rdl.FONTWEIGHT, _fontWeight.Value.ToString());

			//--- Format
			if (_format != null && _format.Value != null && !string.IsNullOrEmpty(_format.Value.ToString()))
				writer.WriteElementString(Rdl.FORMAT, _format.Value.ToString());

			//--- TextDecoration
			if (_textDecoration != null && !string.IsNullOrEmpty(_textDecoration.Value.ToString()))
				writer.WriteElementString(Rdl.TEXTDECORATION, _textDecoration.Value.ToString());

			//--- TextAlign
			if (_textAlign != null && !string.IsNullOrEmpty(_textAlign.Value.ToString()))
				writer.WriteElementString(Rdl.TEXTALIGN, _textAlign.Value.ToString());

			//--- VerticalAlign
			if (_verticalAlign != null && !string.IsNullOrEmpty(_verticalAlign.Value.ToString()))
				writer.WriteElementString(Rdl.VERTICALALIGN, _verticalAlign.Value.ToString());

			//--- Color
			if (_color != null && !string.IsNullOrEmpty(_color.ToString()))
				writer.WriteElementString(Rdl.COLOR, _color.ToString());

			//--- PaddingLeft
			if (_paddingLeft != null && !string.IsNullOrEmpty(_paddingLeft.Value.ToString()))
				writer.WriteElementString(Rdl.PADDINGLEFT, _paddingLeft.Value.ToString());

			//--- PaddingRight
			if (_paddingRight != null && !string.IsNullOrEmpty(_paddingRight.Value.ToString()))
				writer.WriteElementString(Rdl.PADDINGRIGHT, _paddingRight.Value.ToString());

			//--- PaddingTop
			if (_paddingTop != null && !string.IsNullOrEmpty(_paddingTop.Value.ToString()))
				writer.WriteElementString(Rdl.PADDINGTOP, _paddingTop.Value.ToString());

			//--- PaddingBottom
			if (_paddingBottom != null && !string.IsNullOrEmpty(_paddingBottom.Value.ToString()))
				writer.WriteElementString(Rdl.PADDINGBOTTOM, _paddingBottom.Value.ToString());

			//--- LineHeight
			if (_lineHeight != null && !string.IsNullOrEmpty(_lineHeight.Value.ToString()))
				writer.WriteElementString(Rdl.LINEHEIGHT, _lineHeight.Value.ToString());

			//--- Direction
			if (_direction != null && !string.IsNullOrEmpty(_direction.Value.ToString()))
				writer.WriteElementString(Rdl.DIRECTION, _direction.Value.ToString());

			//--- WritingMode
			if (_writingMode != null && !string.IsNullOrEmpty(_writingMode.Value.ToString()))
				writer.WriteElementString(Rdl.WRITINGMODE, _writingMode.Value.ToString());

			//--- Language
			if (_language != null && !string.IsNullOrEmpty(_language.Value.ToString()))
				writer.WriteElementString(Rdl.LANGUAGE, _language.Value.ToString());

			//--- UnicodeBiDi
			if (_unicodeBiDi != null && !string.IsNullOrEmpty(_unicodeBiDi.Value.ToString()))
				writer.WriteElementString(Rdl.UNICODEBIDI, _unicodeBiDi.Value.ToString());

			//--- Calendar
			if (_calendar != null && !string.IsNullOrEmpty(_calendar.Value.ToString()))
				writer.WriteElementString(Rdl.CALENDAR, _calendar.Value.ToString());

			//--- NumeralLanguage
			if (_numeralLanguage != null && !string.IsNullOrEmpty(_numeralLanguage.Value.ToString()))
				writer.WriteElementString(Rdl.NUMERALLANGUAGE, _numeralLanguage.Value.ToString());

			//--- NumeralVariant
			if (_numeralVariant != null && !string.IsNullOrEmpty(_numeralVariant.Value.ToString()))
				writer.WriteElementString(Rdl.NUMERALVARIANT, _numeralVariant.Value.ToString());

			writer.WriteEndElement();
		}
		#endregion

		public static string ColorToString(System.Drawing.Color color)
		{
			string c = color.Name;


			if (color.Name.StartsWith("ff"))
			{
				c = color.Name.Remove(0, 2);
				c = "#" + c;
			}
			else
			{
				// weird RS bug LightGrey instead of LightGray
				if (c == "LightGray") c = "LightGrey";
			}

			return c;

		}
		public static System.Drawing.Color ColorFromString(string c)
		{
			System.Drawing.Color color;

			if (c.StartsWith("#"))
			{
				string red = c.Substring(1, 2);
				string green = c.Substring(3, 2);
				string blue = c.Substring(5, 2);

				color = System.Drawing.Color.FromArgb(Convert.ToInt32(red, 16), Convert.ToInt32(green, 16), Convert.ToInt32(blue, 16));
			}
			else
			{
				// weird RS bug LightGrey instead of LightGray
				if (c == "LightGrey") c = "LightGray";
				color = System.Drawing.Color.FromName(c);
			}

			return color;
		}
	}
}

