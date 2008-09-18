using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// 
	/// </summary>
	public class Expression : IXmlSerializable
	{
		#region Private Variables

		private object	_value;

		#endregion

		public Expression()
		{
			_value = string.Empty;
		}

		public Expression(string value)
		{
			_value = value;
		}

		#region Pulic Properties

		public object Value
		{
			get {return _value;}
			set {_value = value;}
		}

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return _value.ToString();
		}
        public  static bool IsExpression(string text)
        {
            return text.StartsWith("=");
        }
		#endregion

		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
		}

		public void WriteXml(XmlWriter writer)
		{
		}

		#endregion
}
}
