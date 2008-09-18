using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the fields in the data model.
	/// </summary>
	public class FieldCollection : Collection<Field>, IXmlSerializable
	{
        private bool _unqualified = false;
        internal Field this[string name]
        {
            get
            {
                foreach (Field f in this.Items)
                {
                    if (0 == string.Compare(name, f.Name, true))
                    {
                        return f;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// If the type should be qualified.
        /// </summary>
        public bool Unqualified
        {
            get { return _unqualified; }
            set { _unqualified = value; }
        }
        internal new void Add (Field field)
        {
            if (this[field.Name] == null) base.Add(field);
        }
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a FieldCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the FieldCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.FIELDS);

            foreach (IXmlSerializable field in Items)
            {
                ((Field)field).Unqualified = this.Unqualified;
                field.WriteXml(writer);
            }

			writer.WriteEndElement();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			// TODO:  Add GetSchema implementation
			return null;
		}

		/// <summary>
		/// Generates an FieldCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the FieldCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.FIELDS)
			{
				Field field = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.FIELDS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- Field
						if (reader.LocalName == Rdl.FIELD)
						{
							field = new Field();

							((IXmlSerializable)field).ReadXml(reader);

							base.Add(field);
						}
					}
				}
			}
		}

		#endregion
	}
}



