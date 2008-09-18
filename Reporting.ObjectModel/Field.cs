using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains information about a field within the data model of the report.
	/// </summary>
	public class Field : IXmlSerializable
    {
        #region Constants
        internal enum StandardFieldNames
        {
            Item,
            Entity
        }
        internal const string FIELD_SETTINGS = "Settings";
        public const string  TYPENAME_UNQUALIFIED = "TypeName";
        #endregion
        #region Private Variables

        private string		_name;
		private string		_dataField;
		private	Value		_value;
        private Type        _typeName;
        private bool       _unqualified = false;

		#endregion

		/// <summary>
		/// Creates an instance of a Field.
		/// </summary>
		public Field(){}
        /// <summary>
        /// Creates an instance of a Field and sets its state.
        /// </summary>
        public Field(string name, string dataField, Type type) 
        {
            _name = name;
            _dataField = dataField;
            _typeName = type;
        }

		#region Public Properties

		/// <summary>
		/// Name to use for the field within the report.
		/// </summary>
		public string Name
		{
			get {return _name;}
			set {_name = value;}
		}

		/// <summary>
		/// Name of the field in the query.
		/// </summary>
		public string DataField
		{
			get {return _dataField;}
			set {_dataField = value;}
		}
        /// <summary>
        /// If the type should be qualified.
        /// </summary>
        public bool Unqualified
        {
            get { return _unqualified; }
            set { _unqualified = value; }
        }

		/// <summary>
		/// An expression that evaluates to the value of this field.
		/// </summary>
		public Value Value
		{
			get 
			{
				if (_value == null)
					_value = new Value();

				return _value;
			}
			set {_value = value;}
		}
        /// <summary>
        /// An expression that evaluates to the value of this field.
        /// </summary>
        public Type TypeName
        {
            get
            {
                return _typeName;
            }
            set { this._typeName = value; }
        }

		#endregion

		#region IXmlSerializable Members

		/// <summary>
		/// Converts a Field into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Field is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
			//--- Field
			writer.WriteStartElement(Rdl.FIELD);
			writer.WriteAttributeString(Rdl.NAME, _name);

			//--- DataField
			if (_dataField != null)
				writer.WriteElementString(Rdl.DATAFIELD, _dataField);

            //--- TypeName
            
            if (_typeName != null)
                writer.WriteElementString(this.Unqualified?TYPENAME_UNQUALIFIED:Rdl.TYPENAME, _typeName.ToString());

			//--- Value
			if (_value != null)
				((IXmlSerializable)_value).WriteXml(writer);

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
		/// Generates an Field from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Field is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.AttributeCount > 0)
				_name = reader[Rdl.NAME];

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.FIELD)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
                    switch  (reader.Name)
                    {
                        case Rdl.DATAFIELD: _dataField = reader.ReadString(); break;
                        case Rdl.TYPENAME:  _typeName = System.Type.GetType(reader.ReadString()); break;
                        case TYPENAME_UNQUALIFIED: _typeName = System.Type.GetType(reader.ReadString()); break;
                        case Rdl.VALUE: 
					        if (reader.Name == Rdl.VALUE)
					        {
						        if (_value == null)
							        _value = new Value();

						        ((IXmlSerializable)_value).ReadXml(reader);
                            }; break;                      

                    }
                    ////--- DataField
                    //if (reader.Name == Rdl.DATAFIELD)
                    //    _dataField = reader.ReadString();

                    ////--- Value
                    //if (reader.Name == Rdl.VALUE)
                    //{
                    //    if (_value == null)
                    //        _value = new Value();

                    //    ((IXmlSerializable)_value).ReadXml(reader);
                    //}
				}
			}
		}

		#endregion

	}
}
