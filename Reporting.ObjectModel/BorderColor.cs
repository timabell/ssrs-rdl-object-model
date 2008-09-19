using System;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class BorderColor : IXmlSerializable
	{
		#region Private Variables

        private Expression<Color> _default;
        private Expression<Color> _left;
        private Expression<Color> _right;
        private Expression<Color> _top;
        private Expression<Color> _bottom;

		#endregion

		/// <summary>
		/// Creates a new instance of the BorderColor class.
		/// </summary>
		public BorderColor() { }

		/// <summary>
		/// Color of the border.
		/// </summary>
		public Expression<Color> Default
		{
			get
			{
				if (_default == null)
					_default = new Expression<Color>();

				return _default;
			}
			set { _default = value; }
		}

		/// <summary>
		/// Color of the left border.
		/// </summary>
        public Expression<Color> Left
		{
			get
			{
				if (_left == null)
                    _left = new Expression<Color>();

				return _left;
			}
			set { _left = value; }
		}

		/// <summary>
		/// Color of the right border.
		/// </summary>
        public Expression<Color> Right
		{
			get
			{
				if (_right == null)
                    _right = new Expression<Color>();

				return _right;
			}
			set { _right = value; }
		}

		/// <summary>
		/// Color of the top border.
		/// </summary>
        public Expression<Color> Top
		{
			get
			{
				if (_top == null)
                    _top = new Expression<Color>();

				return _top;
			}
			set { _top = value; }
		}

		/// <summary>
		/// Color of the bottom border.
		/// </summary>
        public Expression<Color> Bottom
		{
			get
			{
				if (_bottom == null)
                    _bottom = new Expression<Color>();

				return _bottom;
			}
			set { _bottom = value; }
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
		/// Generates an BorderColor from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the BorderColor is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.BORDERCOLOR)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Default
					if (reader.Name == Rdl.DEFAULT && !reader.IsEmptyElement)
					{
						if (_default == null)
                            _default = new Expression<Color>();

						_default.Value = reader.ReadString();
					}

					//--- Left
					if (reader.Name == Rdl.LEFT && !reader.IsEmptyElement)
					{
						if (_left == null)
                            _left = new Expression<Color>();

						_left.Value = reader.ReadString();
					}

					//--- Right
					if (reader.Name == Rdl.RIGHT && !reader.IsEmptyElement)
					{
						if (_right == null)
                            _right = new Expression<Color>();

						_right.Value = reader.ReadString();
					}

					//--- Top
					if (reader.Name == Rdl.TOP && !reader.IsEmptyElement)
					{
						if (_top == null)
                            _top = new Expression<Color>();

						_top.Value = reader.ReadString();
					}

					//--- Bottom
					if (reader.Name == Rdl.BOTTOM && !reader.IsEmptyElement)
					{
						if (_bottom == null)
                            _bottom = new Expression<Color>();

						_bottom.Value = reader.ReadString();
					}
				}
			}
		}

		/// <summary>
		/// Converts a BorderColor into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the BorderColor is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Style
			writer.WriteStartElement(Rdl.BORDERCOLOR);

			//--- Default
			if (_default != null)
				writer.WriteElementString(Rdl.DEFAULT, _default.ToString());

			//--- Left
			if (_left != null)
				writer.WriteElementString(Rdl.LEFT, _left.ToString());

			//--- Right
			if (_right != null)
				writer.WriteElementString(Rdl.RIGHT, _right.ToString());

			//--- Top
			if (_top != null)
				writer.WriteElementString(Rdl.TOP, _top.ToString());

			//--- Bottom
			if (_bottom != null)
				writer.WriteElementString(Rdl.BOTTOM, _bottom.ToString());

			writer.WriteEndElement();
		}
		#endregion
	}
}

