using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Describes a filter to apply to rows of data in a data set or data region
	/// or to apply to group instances.
	/// </summary>
	public class Filter : IXmlSerializable
	{
		#region Private Variables

		private FilterExpression		_filterExpression;
		private Operator 				_operator;
		private FilterValueCollection	_filterValues;

		#endregion

		/// <summary>
		/// Creates an instance of a Filter.
		/// </summary>
		public Filter(){}

		#region Public Properties

		/// <summary>
		/// An expression the is evaluated for each instance within a group or each 
		/// row of the data set or data region and compared (via the Operator) 
		/// to the FilterValues.
		/// </summary>
		public FilterExpression FilterExpression
		{
			get {return _filterExpression;}
			set {_filterExpression = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public Operator Operator
		{
			get {return _operator;}
			set {_operator = value;}
		}

		/// <summary>
		/// The values to compare to the FilterExpression.
		/// </summary>
		public FilterValueCollection FilterValues
		{
			get {return _filterValues;}
			set {_filterValues = value;}
		}


		#endregion

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
		/// Generates an Filter from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Filter is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.FILTER)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- FilterExpression
					if (reader.Name == Rdl.FILTEREXPRESSION)
					{
						if (_filterExpression == null)
							_filterExpression = new FilterExpression();

						((IXmlSerializable)_filterExpression).ReadXml(reader);
					}

					//--- Operator
					if (reader.Name == Rdl.OPERATOR)
						_operator = (Operator)Enum.Parse(typeof(Operator), reader.ReadString());

					//--- FilterValues
					if (reader.Name == Rdl.FILTERVALUES)
					{
						if (_filterValues == null)
							_filterValues = new FilterValueCollection();

						((IXmlSerializable)_filterValues).ReadXml(reader);
					}
				}
			}
		}

		/// <summary>
		/// Converts a Filter into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Filter is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Body
			writer.WriteStartElement(Rdl.FILTER);

			//--- FilterExpression
			if (_filterExpression != null)
				((IXmlSerializable)_filterExpression).WriteXml(writer);

			//--- Operator
			writer.WriteElementString(Rdl.OPERATOR, _operator.ToString());

			//--- FilterValues
			if (_filterValues != null)
				((IXmlSerializable)_filterValues).WriteXml(writer);

			writer.WriteEndElement();
		}

		#endregion
}
}
