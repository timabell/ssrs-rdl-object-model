using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Image embedded within the report definition.
	/// </summary>
	public class EmbeddedImage : IXmlSerializable
	{
		#region Private Variables

		private string		_name;
		private string		_mimeType;
		private string		_imageData;

		#endregion

		/// <summary>
		/// Creates a new instance of an EmbeddedImage.
		/// </summary>
		public EmbeddedImage(){}

		#region Public Properties

		/// <summary>
		/// Name of the image.
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// The MIMEType for the image.
		/// </summary>
		public string MIMEType
		{
			get {return _mimeType;}
			set {_mimeType = value;}
		}

		/// <summary>
		/// Base-64 encoded image data.
		/// </summary>
		public string ImageData
		{
			get {return _imageData;}
			set {_imageData = value;}
		}

		#endregion

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
		/// Generates an EmbeddedImage from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the EmbeddedImage is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			//--- Name
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.EMBEDDEDIMAGE)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- MIMEType
					if (reader.Name == Rdl.MIMETYPE)
						_mimeType = reader.ReadString();

					//--- ImageData
					if (reader.Name == Rdl.IMAGEDATA)
						_imageData = reader.ReadString();
				}
			}
		}

		/// <summary>
		/// Converts a EmbeddedImage into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the EmbeddedImage is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- EmbeddedImage
			writer.WriteStartElement(Rdl.EMBEDDEDIMAGE);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- MIMEType
			if (_mimeType.Trim() != string.Empty)
				writer.WriteElementString(Rdl.MIMETYPE, _mimeType);

			//--- ImageData
			if (_imageData.Trim() != string.Empty)
				writer.WriteElementString(Rdl.IMAGEDATA, _imageData);

			writer.WriteEndElement();
		}

		#endregion
}
}
