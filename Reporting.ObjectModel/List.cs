using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Reporting.ObjectModel
{
    public class List : ReportItem
    {
        private ReportItemCollection	_reportItems;	
        //Grouping and sorting...
        private Grouping _grouping;

		/// <summary>
		/// Creates a new instance of a Body.
		/// </summary>
		public List()
		{
			
		}

        public List(string name)
            : this()
        {
            this._name = name;
        }

		/// <summary>
		/// The region that contains the elements of the report body.
		/// </summary>
		public ReportItemCollection ReportItems
		{
			get 
			{
                
				if(_reportItems == null)
					_reportItems = new ReportItemCollection();

				return _reportItems;
			}

			set {_reportItems = value;}
		}

		/// <summary>
		/// Default style information for the body.
		/// </summary>
		public Style Style
		{
			get {return _style;}
			set {_style = value;}
		}

        protected override string GetRootElement()
        {
            return Rdl.LIST;
        }

        /// <summary>
        /// The expressions to group the detail data by.
        /// </summary>
        public Grouping Grouping
        {
            get
            {
                if (_grouping == null)
                    _grouping = new Grouping();

                return _grouping;
            }
            set { _grouping = value; }
        }

		/// <summary>
		/// Generates an Body from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Body is deserialized</param>
        protected override void ReadRDL(XmlReader reader)
		{
			while (reader.Read())
			{
                base.ReadRDL(reader);

                //if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.LIST)
                //{
                //    break;
                //}
                //--- Grouping
                //if (reader.Name == Rdl.GROUPING)
                //{
                //    if (_grouping == null)
                //        _grouping = new Grouping();

                //    ((IXmlSerializable)_grouping).ReadXml(reader);
                //}
                //else if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
                //{
                //    //--- Report Items
                //    if (reader.Name == Rdl.REPORTITEMS && !reader.IsEmptyElement)
                //    {
                //        if (_reportItems == null)
                //            _reportItems = new ReportItemCollection();

                //        ((IXmlSerializable)_reportItems).ReadXml(reader);
                //    }

                //    ////--- Style
                //    //if (reader.Name == Rdl.STYLE && !reader.IsEmptyElement)
                //    //{
                //    //    if (_style == null)
                //    //        _style = new Style();

                //    //    ((IXmlSerializable)_style).ReadXml(reader);
                //    //}
                //}
			}
		}

		/// <summary>
		/// Converts a Body into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Body is serialized</param>
        protected override void WriteRDL(XmlWriter writer)
		{

			//--- ReportItems
			if (_reportItems != null)
				((IXmlSerializable)_reportItems).WriteXml(writer);

            //--- Grouping
            if (_grouping != null)
                ((IXmlSerializable)_grouping).WriteXml(writer);

            base.WriteRDL(writer);
		}
    }
}
