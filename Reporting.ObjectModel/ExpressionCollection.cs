using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class ExpressionCollection : CollectionBase, IXmlSerializable
	{
		public ExpressionCollection() : base(){}

		#region Constructors

		public Expression this[int index]  
		{
			get {return((Expression)List[index]);}
			set {List[index] = value;}
		}

		#endregion

		#region IList Implementation

		public int Add(Expression value)  
		{
			return (List.Add(value));
		}

		public int IndexOf(Expression value)  
		{
			return(List.IndexOf(value));
		}

		public void Insert(int index, Expression value)  
		{
			List.Insert(index, value);
		}

		public void Remove(Expression value)  
		{
			List.Remove(value);
		}

		public bool Contains(Expression value)  
		{
			return(List.Contains(value));
		}

		#endregion

		#region CollectionBase Overrides

		protected override void OnValidate(Object value)  
		{
			if ( value.GetType() != Type.GetType("Reporting.ObjectModel.Expression") )
				throw new ArgumentException( "value must be of type Expression.", "value" );
		}

		#endregion

		#region IXmlSerializable Members

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Expressions");

			foreach(IXmlSerializable expression in List)
				expression.WriteXml(writer);

			writer.WriteEndElement();
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			// TODO:  Add GetSchema implementation
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			// TODO:  Add ReadXml implementation
		}

		#endregion
	}
}





