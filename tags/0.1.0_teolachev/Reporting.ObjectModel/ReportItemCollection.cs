using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Collection of report items (used to define the contents of a region of a report).
	/// </summary>
	public class ReportItemCollection : Collection<ReportItem>, IXmlSerializable
	{
  
        /// <summary>
        /// Hides the base accessor to allow item replace
        /// </summary>
        /// <param name="index">The index of the item to be replaced</param>
        /// <returns>The report item found that the specified index</returns>
        public new ReportItem this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base.SetItem(index, value);
            }
        }


        /// <summary>
        /// Gets the occupied width of the collection
        /// </summary>
        public Size Width
        {
            get
            {
                Size rightMargin = new Size(0);

                foreach (ReportItem item in this.Items)
                {
                    if (item.Left + item.Width > rightMargin) rightMargin = item.Left + item.Width;
                }
                return rightMargin;
            }
        }

        //public new void Add(ReportItem item)
        //{
        //    base.Add(item)

        //}
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
		/// Generates an ReportItemCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the ReportItemCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.REPORTITEMS)
			{
				ReportItem reportItem = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.REPORTITEMS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- ReportItem
						switch (reader.LocalName)
						{
						    //--- Line
						    case Rdl.LINE:
						    {
						        reportItem = new Line();

						        break;
						    }

						    //--- Rectangle
						    case Rdl.RECTANGLE:
						    {
						        reportItem = new Rectangle();

						        break;
						    }

						    //--- Textbox
						    case Rdl.TEXTBOX:
						    {
						        reportItem = new Textbox();

						        break;
						    }

						    //--- Image
						    case Rdl.IMAGE:
						    {
						        reportItem = new Image();

						        break;
						    }


						    //--- Table
						    case Rdl.TABLE:
						    {
						        reportItem = new Table();

						        break;
						    }

						}

						if (reportItem != null)
						{
							((IXmlSerializable)reportItem).ReadXml(reader);

							Add(reportItem);
						}
					}
				}
			}
		}

		/// <summary>
		/// Converts a ReportItemCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the ReportItemCollection is serialized</param>
		public void WriteXml(XmlWriter writer)
		{
            if (this.Items.Count == 0) return;

			writer.WriteStartElement(Rdl.REPORTITEMS);

			foreach (IXmlSerializable reportItem in Items)
				if (reportItem != null) reportItem.WriteXml(writer);

			writer.WriteEndElement();
		}
    
		#endregion
	}
}

