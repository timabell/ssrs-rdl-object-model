using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Collection of images embedded within the report definition.
	/// </summary>
	public class EmbeddedImageCollection : Collection<EmbeddedImage>, IXmlSerializable
	{
        public EmbeddedImage this[string name]
        {
            get
            {
                foreach (EmbeddedImage image in Items)
                {
                    if (image.Name == name)
                        return image;
                }

                return null;
            }
        }

        public bool Contains(string name)
        {
            foreach (EmbeddedImage image in Items)
            {
                if (image.Name == name)
                    return true;
            }

            return false;
        }

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a EmbeddedImage into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the EmbeddedImage is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.EMBEDDEDIMAGES);

			foreach(IXmlSerializable embeddedImage in Items)
				embeddedImage.WriteXml(writer);

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
		/// Generates an EmbeddedImage from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the EmbeddedImage is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.EMBEDDEDIMAGES)
			{
				EmbeddedImage embeddedImage = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.EMBEDDEDIMAGES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- EmbeddedImage
						if (reader.LocalName == Rdl.EMBEDDEDIMAGE)
						{
							embeddedImage = new EmbeddedImage();

							((IXmlSerializable)embeddedImage).ReadXml(reader);

							Add(embeddedImage);
						}
					}
				}
			}
		}

		#endregion
	}
}

