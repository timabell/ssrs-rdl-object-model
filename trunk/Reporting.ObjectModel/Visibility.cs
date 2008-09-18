using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{

	public class Visibility : IXmlSerializable
	{
		#region Private Variables

		private Expression	_hidden;
		private string		_toggleItem;

		#endregion

		/// <summary>
		/// Creates an instance of a Visibility class.
		/// </summary>
		public Visibility()
        {

        }
        public Visibility(string exp):this()
        {
            _hidden = new Expression(exp);
        }

		#region Public Properties

		/// <summary>
		/// Inidicates if the item should initially be hidden.
		/// </summary>
		public Expression Hidden
		{
			get 
            {
                if (_hidden == null)
                {
                    _hidden = new Expression();
                    _hidden.Value = "false";
                }

                return _hidden;
            }
			set {_hidden = value;}
		}

		/// <summary>
		/// The name of the Textbox used to hide/unhide a report item.
		/// </summary>
		public string ToggleItem
		{
			get {return _toggleItem;}
			set {_toggleItem = value;}
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
		/// Generates an instance of a Visibility from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Visibility is deserialized</param>
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.VISIBILITY)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Hidden
					if (reader.Name == Rdl.HIDDEN)
					{
						if (_hidden == null)
							_hidden = new Expression();

						_hidden.Value = reader.ReadString();
					}

					//--- ToggleItem
					if (reader.Name == Rdl.TOGGLEITEM)
						_toggleItem = reader.ReadString();
				}
			}
		}

		/// <summary>
		/// Converts a Visibility into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Visibility is serialized.</param>
		public void WriteXml(XmlWriter writer)
		{
            if (_hidden == null && string.IsNullOrEmpty(_toggleItem)) return;
			//--- Visibility
			writer.WriteStartElement(Rdl.VISIBILITY);

			//--- Hidden
			if (_hidden != null)
				writer.WriteElementString(Rdl.HIDDEN, _hidden.Value.ToString());

			//--- ToggleItem
			if(_toggleItem != null && _toggleItem != string.Empty)
				writer.WriteElementString(Rdl.TOGGLEITEM, _toggleItem);

			writer.WriteEndElement();
		}

		#endregion
}
}
