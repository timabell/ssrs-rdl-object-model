using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class BorderWidth : IXmlSerializable
	{
		#region Private Variables

		private Expression _default;
		private Expression _left;
		private Expression _right;
		private Expression _top;
		private Expression _bottom;

		#endregion

		/// <summary>
		/// Creates a new instance of the BorderWidth class.
		/// </summary>
		public BorderWidth() { }

		/// <summary>
		/// Color of the border.
		/// </summary>
		public Expression Default
		{
			get
			{
				if (_default == null)
					_default = new Expression();

				return _default;
			}
			set { _default = value; }
		}

		/// <summary>
		/// Color of the left border.
		/// </summary>
		public Expression Left
		{
			get
			{
				if (_left == null)
					_left = new Expression();

				return _left;
			}
			set { _left = value; }
		}

		/// <summary>
		/// Color of the right border.
		/// </summary>
		public Expression Right
		{
			get
			{
				if (_right == null)
					_right = new Expression();

				return _right;
			}
			set { _right = value; }
		}

		/// <summary>
		/// Color of the top border.
		/// </summary>
		public Expression Top
		{
			get
			{
				if (_top == null)
					_top = new Expression();

				return _top;
			}
			set { _top = value; }
		}

		/// <summary>
		/// Color of the bottom border.
		/// </summary>
		public Expression Bottom
		{
			get
			{
				if (_bottom == null)
					_bottom = new Expression();

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
		/// Generates an BorderWidth from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the BorderWidth is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.BORDERWIDTH)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Default
					if (reader.Name == Rdl.DEFAULT && !reader.IsEmptyElement)
					{
						if (_default == null)
							_default = new Expression();

						_default.Value = reader.ReadString();
					}

					//--- Left
					if (reader.Name == Rdl.LEFT && !reader.IsEmptyElement)
					{
						if (_left == null)
							_left = new Expression();

						_left.Value = reader.ReadString();
					}

					//--- Right
					if (reader.Name == Rdl.RIGHT && !reader.IsEmptyElement)
					{
						if (_right == null)
							_right = new Expression();

						_right.Value = reader.ReadString();
					}

					//--- Top
					if (reader.Name == Rdl.TOP && !reader.IsEmptyElement)
					{
						if (_top == null)
							_top = new Expression();

						_top.Value = reader.ReadString();
					}

					//--- Bottom
					if (reader.Name == Rdl.BOTTOM && !reader.IsEmptyElement)
					{
						if (_bottom == null)
							_bottom = new Expression();

						_bottom.Value = reader.ReadString();
					}
				}
			}
		}

		/// <summary>
		/// Converts a BorderWidth into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the BorderWidth is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Style
			writer.WriteStartElement(Rdl.BORDERWIDTH);

			//--- Default
			if (_default != null)
				writer.WriteElementString(Rdl.DEFAULT, _default.Value.ToString());

			//--- Left
			if (_left != null)
				writer.WriteElementString(Rdl.LEFT, _left.Value.ToString());

			//--- Right
			if (_right != null)
				writer.WriteElementString(Rdl.RIGHT, _right.Value.ToString());

			//--- Top
			if (_top != null)
				writer.WriteElementString(Rdl.TOP, _top.Value.ToString());

			//--- Bottom
			if (_bottom != null)
				writer.WriteElementString(Rdl.BOTTOM, _bottom.Value.ToString());

			writer.WriteEndElement();
		}
		#endregion
	}
}

