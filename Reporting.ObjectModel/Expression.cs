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

		protected object	_value;

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

	public class Expression<T> : Expression
	{
		public Expression() : base()
		{
		}

		public Expression(string value) : base(value)
		{
		}

		public static implicit operator Expression<T>(T o)
		{
			Expression<T> e = new Expression<T>();
			e._value = o;
			return e;
		}

		public override string ToString()
		{
			string tmp = this._value.ToString();
			if (typeof(T) == typeof(System.Drawing.Color))
				if (!IsExpression(tmp) && _value is System.Drawing.Color)
					return Style.ColorToString((System.Drawing.Color)_value);
			return tmp;
		}
	}
}
