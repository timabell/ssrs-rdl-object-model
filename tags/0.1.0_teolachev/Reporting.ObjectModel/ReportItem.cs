using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class ReportItem : IXmlSerializable
	{
		#region Private Variables

		protected string _name;
		protected Style _style;
		protected Size _top;
		protected Size _left;
		protected Size _height;
		protected Size _width;
		protected int _zIndex;
		protected Visibility _visibility;
		protected Expression _toolTip;
		protected Expression _label;
		protected string _linkToChild;
		protected Expression _bookmark;
		protected string _repeatWith;
		protected CustomPropertiesCollection _customProperties;
		protected string _dataElementName;
		protected DataElementOutput _dataElementOutput;

		#endregion

		/// <summary>
		/// Creates an instance of a ReportItem.
		/// </summary>
		public ReportItem() 
        { 
        }

		/// <summary>
		/// Name of the report item.
		/// </summary>
		public virtual string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Style information for the report item.
		/// </summary>
		public virtual Style Style
		{
			get 
			{
				if (_style == null)
					_style = new Style();

				return _style; 
			}
			set { _style = value; }
		}


		/// <summary>
		/// The distance of the item from the top of the containing object.
		/// </summary>
		public virtual Size Top
		{
			get { return _top; }
			set { _top = value; }
		}

		/// <summary>
		/// The distance of the item from the left of the containing object.
		/// </summary>
		public virtual Size Left
		{
			get { return _left; }
			set { _left = value; }
		}

		/// <summary>
		/// Height of the item.
		/// </summary>
		public virtual Size Height
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Width of the item.
		/// </summary>
		public virtual Size Width
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Drawing order of the report item within the containing object.
		/// </summary>
		public virtual int ZIndex
		{
			get { return _zIndex; }
			set { _zIndex = value; }
		}

		/// <summary>
		/// Indicates if the item should be hidden.
		/// </summary>
		public virtual Visibility Visibility
		{
			get 
            {
                if (_visibility == null)
                    _visibility = new Visibility();

                return _visibility; 
            }
			set { _visibility = value; }
		}

		/// <summary>
		/// A textual label for the report item.
		/// </summary>
		public virtual Expression ToolTip
		{
			get { return _toolTip; }
			set { _toolTip = value; }
		}

		/// <summary>
		/// A label to identify an instance of a report item within the client UI.
		/// </summary>
		public virtual Expression Label
		{
			get {
                if (_label == null) _label = new Expression();
                return _label; 
            }
			set { _label = value; }
		}

		/// <summary>
		/// The name of the report item contained directly within this report 
		/// item that is the target location for the Document Map label.
		/// </summary>
		public virtual string LinkToChild
		{
			get { return _linkToChild; }
			set { _linkToChild = value; }
		}

		/// <summary>
		/// A bookmark that can be linked to via a bookmark action.
		/// </summary>
		public virtual Expression Bookmark
		{
			get { return _bookmark; }
			set { _bookmark = value; }
		}

		/// <summary>
		/// The name of a data region that this report item should be repeated 
		/// with if that data region spans multiple pages.
		/// </summary>
		public virtual string RepeatWith
		{
			get { return _repeatWith; }
			set { _repeatWith = value; }
		}

		public virtual CustomPropertiesCollection CustomProperties
		{
			get { return _customProperties; }
			set { _customProperties = value; }
		}

		/// <summary>
		/// The name to use for the data element/attribute for this report item.
		/// </summary>
		public virtual string DataElementName
		{
			get { return _dataElementName; }
			set { _dataElementName = value; }
		}

		/// <summary>
		/// Indicates whether the item should appear in a data rendering.
		/// </summary>
		public virtual DataElementOutput DataElementOutput
		{
			get { return _dataElementOutput; }
			set { _dataElementOutput = value; }
		}

		#region Protected Methods

        protected virtual string GetRootElement() { return null; }

        protected virtual void ReadRDL(XmlReader reader){}

        protected virtual void WriteRDL(XmlWriter writer){}

		#endregion

		#region IXmlSerializable Members

        internal virtual bool ShouldSerialize
        {
            get { return true; }
        }

		/// <summary>
		/// Converts a ReportItem into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ReportItem is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
            if (!ShouldSerialize) return;

			//--- Root Element
			writer.WriteStartElement(GetRootElement());
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- Delegate to the implementing class for any additional attributes
			WriteRDL(writer);

			//--- Style
			if (_style != null)
				((IXmlSerializable)_style).WriteXml(writer);
			else
				writer.WriteElementString(Rdl.STYLE, string.Empty);

			//--- Top
			if (!_top.IsEmpty && _top.ToString() != "0in")
				writer.WriteElementString(Rdl.TOP, _top.ToString());

			//--- Left
			if (!_left.IsEmpty && _left.ToString() != "0in")
				writer.WriteElementString(Rdl.LEFT, _left.ToString());

			//--- Height
			if (!_height.IsEmpty)
				writer.WriteElementString(Rdl.HEIGHT, _height.ToString());

			//--- Width
			if (!_width.IsEmpty)
				writer.WriteElementString(Rdl.WIDTH, _width.ToString());

			//--- ZIndex
			if(_zIndex != 0)
				writer.WriteElementString(Rdl.ZINDEX, _zIndex.ToString());

			//--- Visibility
			if (_visibility != null)
				((IXmlSerializable)_visibility).WriteXml(writer);

			//--- ToolTip
			if (_toolTip != null)
				writer.WriteElementString(Rdl.TOOLTIP, _toolTip.Value.ToString());

			//--- Label
			if (_label != null)
				writer.WriteElementString(Rdl.LABEL, _label.Value.ToString());

			//--- LinkToChild
			if (_linkToChild != null && _linkToChild != string.Empty)
				writer.WriteElementString(Rdl.LINKTOCHILD, _linkToChild);

			//--- Bookmark
			if (_bookmark != null)
				writer.WriteElementString(Rdl.BOOKMARK, _bookmark.Value.ToString());

			//--- RepeatWith
			if(_repeatWith != null && _repeatWith != string.Empty)
				writer.WriteElementString(Rdl.REPEATWITH, _repeatWith);

			//--- CustomProperties
			if (_customProperties != null)
				((IXmlSerializable)_customProperties).WriteXml(writer);

			//--- DataElementName
			if(_dataElementName != null && _dataElementName != string.Empty && _dataElementName != _name)
				writer.WriteElementString(Rdl.DATAELEMENTNAME, _dataElementName);

			//--- DataElementOutput
			if(_dataElementOutput != DataElementOutput.Auto)
				writer.WriteElementString(Rdl.DATAELEMENTOUTPUT, _dataElementOutput.ToString());

			writer.WriteEndElement();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Generates an ReportItem from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ReportItem is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

            if (reader.IsEmptyElement)
                return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == GetRootElement())
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
				{
                    //--- Delegate to the implementing class for any additional attributes
                    ReadRDL(reader);

					//--- Style
					if (reader.Name == Rdl.STYLE)
					{
						if (_style == null)
							_style = new Style();

						((IXmlSerializable)_style).ReadXml(reader);
					}


					//--- Top
					if (reader.Name == Rdl.TOP)
						_top = Size.Parse(reader.ReadString());

					//--- Left
					if (reader.Name == Rdl.LEFT)
						_left = Size.Parse(reader.ReadString());

					//--- Height
					if (reader.Name == Rdl.HEIGHT)
						_height = Size.Parse(reader.ReadString());

					//--- Width
					if (reader.Name == Rdl.WIDTH)
						_width = Size.Parse(reader.ReadString());

					//--- ZIndex
					if (reader.Name == Rdl.ZINDEX)
						_zIndex = int.Parse(reader.ReadString());

					//--- Visibility
					if (reader.Name == Rdl.VISIBILITY)
					{
						if (_visibility == null)
							_visibility = new Visibility();

						((IXmlSerializable)_visibility).ReadXml(reader);
					}

					//--- ToolTip
					if (reader.Name == Rdl.TOOLTIP)
					{
						if (_toolTip == null)
							_toolTip = new Expression();

						_toolTip.Value = reader.ReadString();
					}

					//--- Label
					if (reader.Name == Rdl.LABEL)
					{
						if (_label == null)
							_label = new Expression();

						_label.Value = reader.ReadString();
					}

					//--- LinkToChild
					if (reader.Name == Rdl.LINKTOCHILD)
						_linkToChild = reader.ReadString();

					//--- Bookmark
					if (reader.Name == Rdl.BOOKMARK)
					{
						if (_bookmark == null)
							_bookmark = new Expression();

						_bookmark.Value = reader.ReadString();
					}

					//--- RepeatWith
					if (reader.Name == Rdl.REPEATWITH)
						_repeatWith = reader.ReadString();

					//--- CustomProperties
					if (reader.Name == Rdl.CUSTOMPROPERTIES)
					{
						if (_customProperties == null)
							_customProperties = new CustomPropertiesCollection();

						((IXmlSerializable)_customProperties).ReadXml(reader);
					}

					//--- DataElementName
					if (reader.Name == Rdl.DATAELEMENTNAME)
						_dataElementName = reader.ReadString();

					//--- DataElementOutput
					if (reader.Name == Rdl.DATAELEMENTOUTPUT)
						_dataElementOutput = (DataElementOutput)Enum.Parse(typeof(DataElementOutput), reader.ReadString());


				}
			}
		}


		#endregion
	}
}
