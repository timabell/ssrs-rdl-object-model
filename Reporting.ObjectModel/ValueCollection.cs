using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains and ordered list of parameters for the report.
	/// </summary>
	public class ValueCollection : Collection<Expression>, IXmlSerializable
	{
		/// <summary>
		/// Creates a new instance of a ValueCollection.
		/// </summary>
		public ValueCollection() : base() { }
        public ValueCollection(string[] values) : this() 
        {
            foreach (string value in values)
            {
                this.Add(new Expression(value));
            }
        }

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a ValueCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ValueCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.VALUES);

			foreach (Expression expression in Items)
				writer.WriteElementString(Rdl.VALUE, expression.Value.ToString());

			writer.WriteEndElement();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			// TODO:  Add ValueCollection.GetSchema implementation
			return null;
		}

		/// <summary>
		/// Generates an ValueCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ValueCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.VALUES)
			{
				Expression value = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.VALUES)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- Value
						if (reader.LocalName == Rdl.VALUE)
						{
							value = new Expression();

							value.Value = reader.ReadString();

							Add(value);
						}
					}
				}
			}
		}

		#endregion
	}
}
