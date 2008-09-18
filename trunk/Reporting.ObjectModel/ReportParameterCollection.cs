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
	public class ReportParameterCollection : Collection<ReportParameter>, IXmlSerializable
	{
		/// <summary>
		/// Creates a new instance of a ReportParameterCollection.
		/// </summary>
		public ReportParameterCollection() : base(){}

        public ReportParameter this[string name]
        {
            get
            {
                foreach (ReportParameter p in this.Items)
                {
                    if (0 == string.Compare(name, p.Name, true))
                    {
                        return p;
                    }
                }
                return null;
            }
        }
		#region IXmlSerializable Members

		/// <summary>
		/// Converts a ReportParameterCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ReportParameterCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
            if (Items.Count == 0) return;

			writer.WriteStartElement(Rdl.REPORTPARAMETERS);

			foreach(IXmlSerializable reportParameter in Items)
				reportParameter.WriteXml(writer);

			writer.WriteEndElement();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			// TODO:  Add ReportParameterCollection.GetSchema implementation
			return null;
		}

		/// <summary>
		/// Generates an ReportParameterCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ReportParameterCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.REPORTPARAMETERS)
			{
				ReportParameter reportParameter = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.REPORTPARAMETERS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- ReportParameter
						if (reader.LocalName == Rdl.REPORTPARAMETER)
						{
							reportParameter = new ReportParameter();

							((IXmlSerializable)reportParameter).ReadXml(reader);

							Add(reportParameter);
						}
					}
				}
			}
		}

		#endregion
	}
}
