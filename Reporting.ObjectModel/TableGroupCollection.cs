using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines the groups in the table.
	/// </summary>
	public class TableGroupCollection : Collection<TableGroup>, IXmlSerializable
	{
        /// <summary>
        /// Returns the header height
        /// </summary>
        public Size Height
        {
            get
            {
                Size height = new Size(0);
                
                foreach (TableGroup g in this.Items)
                {
                    if (g.Header!=null) height += g.Header.Height;
                    if (g.Footer != null) height += g.Footer.Height;
                    
                }
                return height;
            }
        }
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a TableGroupCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the TableGroupCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.TABLEGROUPS);

			foreach (IXmlSerializable tableGroup in Items)
				tableGroup.WriteXml(writer);

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
		/// Generates an TableGroupCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the TableGroupCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.TABLEGROUPS)
			{
				TableGroup tableGroup = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.TABLEGROUPS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- TableGroup
						if (reader.LocalName == Rdl.TABLEGROUP)
						{
							tableGroup = new TableGroup();

							((IXmlSerializable)tableGroup).ReadXml(reader);

							Add(tableGroup);
						}
					}
				}
			}
		}

		#endregion
	}
}


