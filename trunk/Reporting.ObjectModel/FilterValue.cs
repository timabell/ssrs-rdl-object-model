using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Reporting.ObjectModel
{
	public class FilterValue : Expression
	{
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(Rdl.FILTERVALUE);
            base.WriteXml(writer);
            writer.WriteEndElement();
        }
	}
}
