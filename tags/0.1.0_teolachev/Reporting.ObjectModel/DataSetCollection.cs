using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about the sets of data retrieved as a part of the report.
	/// </summary>
	public class DataSetCollection : Collection<DataSet>, IXmlSerializable
	{
		#region IXmlSerializable Members

		
        public DataSet this[string name]
        {
            get
            {
                foreach (DataSet ds in this.Items)
                {
                    if (0 == string.Compare(name, ds.Name, true))
                    {
                        return ds;
                    }
                }
                return null;
            }
        }
        /// <summary>
		/// Converts a DataSetCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the DataSetCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.DATASETS);

			foreach(IXmlSerializable dataSet in Items)
				dataSet.WriteXml(writer);

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
		/// Generates an DataSetCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the DataSetCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.DATASETS)
			{
				DataSet dataSet = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.DATASETS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- DataSet
						if (reader.LocalName == Rdl.DATASET)
						{
							dataSet = new DataSet();

							((IXmlSerializable)dataSet).ReadXml(reader);

							Add(dataSet);
						}
					}
				}
			}
		}

		#endregion
	}
}


