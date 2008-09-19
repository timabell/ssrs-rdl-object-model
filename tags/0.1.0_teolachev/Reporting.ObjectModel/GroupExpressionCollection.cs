using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines an ordered list of expressions to group the data by
	/// </summary>
	public class GroupExpressionCollection : Collection<Expression>, IXmlSerializable
	{
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
		/// Generates an GroupExpressionCollection from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the GroupExpressionCollection is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == Rdl.GROUPEXPRESSIONS)
			{
				Expression groupExpression = null;

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.GROUPEXPRESSIONS)
					{
						break;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						//--- GroupExpression
						if (reader.LocalName == Rdl.GROUPEXPRESSION)
						{
							groupExpression = new Expression();

                            string value = reader.ReadString();
							groupExpression.Value = value;

							Add(groupExpression);
						}
					}
				}
			}
		}

		/// <summary>
		/// Converts a GroupExpressionCollection into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the GroupExpressionCollection is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(Rdl.GROUPEXPRESSIONS);

			foreach (Expression groupExpression in Items)
				if (groupExpression.Value!=null) writer.WriteElementString(Rdl.GROUPEXPRESSION, groupExpression.Value.ToString());

			writer.WriteEndElement();
		}

		#endregion
	}
}

