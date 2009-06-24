using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.ObjectModel
{
	public class FilterExpression : Expression
	{
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement(Rdl.FILTEREXPRESSION);
            base.WriteXml(writer);
            writer.WriteEndElement();
        }
	}
}
