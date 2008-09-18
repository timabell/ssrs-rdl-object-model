using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Contains property, data and layout information about the report.
	/// </summary>
    [XmlRoot("Report", Namespace = Report.RDL_SCHEMA_2005)]
    public class Report : IXmlSerializable
	{
		#region Constants

		public const string     RDL_SCHEMA          = "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition";
		public const string     RDL_SCHEMA_2005     = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
        public const string     RDL_DESIGNER        = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";

		#endregion

		private string						_description;
		private string						_author;
		private int							_autoRefresh;
		private DataSourceCollection		_dataSources;
		private DataSetCollection			_dataSets;
		private Body						_body;
		private ReportParameterCollection	_parameters;
		private string						_code;
		private Size						_width;
		private PageHeader					_pageHeader;
		private PageFooter					_pageFooter;
		private Size						_pageHeight;
		private Size						_pageWidth;
		private Size						_interactiveHeight;
		private Size						_interactiveWidth;
		private Size						_leftMargin = new Size("0.5in");
		private Size						_rightMargin = new Size("0.5in");
		private Size						_topMargin = new Size("0.5in");
		private Size 						_bottomMargin = new Size("0.5in");
		private EmbeddedImageCollection		_embeddedImages;
		private string						_language;
		private CustomPropertiesCollection  _customProperties;
		private string						_dataTransform;
		private string						_dataSchema;
		private string						_dataElementName;
		private ReportDataElementStyle		_dataElementStyle;


        #region Constructors

        /// <summary>
		/// Creates a new instance of a Report.
		/// </summary>
		public Report()
		{
			_body = new Body();

            // default to Landscape
            _pageHeight = new Size("8in");
            _pageWidth = new Size("11in");
            _interactiveHeight = new Size("8in");
            _interactiveWidth = new Size("11in");
            _leftMargin = new Size("0.5in");
            _rightMargin = new Size("0.5in");
            _bottomMargin = new Size("0.5in");
            _topMargin = new Size("0.5in");
		}
        /// <summary>
        /// Deserializes from an existing RDL definition
        /// </summary>
        /// <param name="reader">XmlReader that holds the definition</param>
        public Report(XmlReader reader):this()
        {
            reader.MoveToContent();
            this.ReadXml(reader);
        }
        /// <summary>
        /// Deserializes from an existing RDL definition passed as a byte array
        /// </summary>
        /// <param name="definition">The report definition</param>
        public Report(byte[] definition) : this()
        {
            XmlDocument doc = new XmlDocument();
            MemoryStream stream = new MemoryStream(definition);
            doc.Load(stream);
            XmlNodeReader reader = new XmlNodeReader(doc);
            this.ReadXml(reader);
        }
		#endregion

		#region Public Properties

		/// <summary>
		/// Decription of the Report
		/// </summary>
		public string Description
		{
			get {return _description;}
			set {_description = value;}
		}


		/// <summary>
		/// Author of the Report
		/// </summary>
		public string Author
		{
			get {return _author;}
			set {_author = value;}
		}

			
		/// <summary>
		/// Rate a which the report page automatically refreshes, in seconds.
		/// Must be nonnegative.
		/// </summary>
		public int AutoRefresh
		{
			get {return _autoRefresh;}
			set {_autoRefresh = value;}
		}

		/// <summary>
		/// Describes the data sources from which the data sets are taken for this report.
		/// </summary>
		public DataSourceCollection DataSources
		{
			get 
			{
				if(_dataSources == null)
					_dataSources = new DataSourceCollection();

				return _dataSources;
			}

			set {_dataSources = value;}
		}

		/// <summary>
		/// Describes the data that is displayed as part of the report.
		/// </summary>
		public DataSetCollection DataSets
		{
			get 
			{
				if(_dataSets == null)
					_dataSets = new DataSetCollection();

				return _dataSets;
			}
			
			set {_dataSets = value;}
		}

		/// <summary>
		/// Describes how the body of the report is structured.
		/// </summary>
		public Body Body
		{
			get {return _body;}
			set {_body = value;}
		}

		/// <summary>
		/// Parameters for the report.
		/// </summary>
		public ReportParameterCollection Parameters
		{
			get 
			{
				if(_parameters == null)
					_parameters = new ReportParameterCollection();

				return _parameters;
			}

			set {_parameters = value;}
		}



		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			get {return _code;}
			set {_code = value;}
		}

		/// <summary>
		/// Width of the report.
		/// </summary>
		public Size Width
		{
			get {return _width;}
			set {_width = value;}
		}

		/// <summary>
		/// The header that is output at the top of each page of the report.
		/// </summary>
		public PageHeader PageHeader
		{
			get {
                if (_pageHeader==null) _pageHeader = new PageHeader();
                return _pageHeader;
            }
			set {_pageHeader = value;}
		}

		/// <summary>
		/// The footer that is output at the bottom of each page of the report.
		/// </summary>
		public PageFooter PageFooter
		{
			get 
            {
                if (_pageFooter == null) _pageFooter = new PageFooter(this);
                return _pageFooter;
            }
			set {_pageFooter = value;}
		}

		/// <summary>
		/// Default height for the report.
		/// </summary>
		public Size PageHeight
		{
			get {return _pageHeight;}
			set 
            {
                _pageHeight = value;
                _interactiveHeight = value;
            }
		}

		/// <summary>
		/// Default width for the report.
		/// </summary>
		public Size PageWidth
		{
			get 
            {
                return _pageWidth;
            }
			set {
                _pageWidth = value;
                _interactiveWidth = value;
                _width = BodyWidth; // set the report body width
                }
		}

        private Size BodyWidth
        {
            get
            {
                return _pageWidth - (_leftMargin + _rightMargin);
            }
        }
		/// <summary>
		/// 
		/// </summary>
		public Size InteractiveHeight
		{
			get { return _interactiveHeight; }
			set { _interactiveHeight = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Size InteractiveWidth
		{
			get { return _interactiveWidth; }
			set { _interactiveWidth = value; }
		}

		/// <summary>
		/// Width of the left margin.
		/// </summary>
		public Size LeftMargin
		{
			get {return _leftMargin;}
			set {_leftMargin = value;}
		}

		/// <summary>
		/// Width of the right margin.
		/// </summary>
		public Size RightMargin
		{
			get {return _rightMargin;}
			set {_rightMargin = value;}
		}

		/// <summary>
		/// Width of the top margin.
		/// </summary>
		public Size TopMargin
		{
			get {return _topMargin;}
			set {_topMargin = value;}
		}

		/// <summary>
		/// Width of the bottom margin.
		/// </summary>
		public Size BottomMargin
		{
			get {return _bottomMargin;}
			set {_bottomMargin = value;}
		}

		/// <summary>
		/// Images embedded within the report.
		/// </summary>
		public EmbeddedImageCollection EmbeddedImages
		{
			get 
            {
                if (_embeddedImages == null)
                    _embeddedImages = new EmbeddedImageCollection();

                return _embeddedImages;
            }
			set {_embeddedImages = value;}
		}

		/// <summary>
		/// The primary language of the text.
		/// </summary>
		public string Language
		{
			get {return _language;}
			set {_language = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public CustomPropertiesCollection CustomProperties
		{
			get 
			{
				if (_customProperties == null)
					_customProperties = new CustomPropertiesCollection();

				return _customProperties; 
			}
			set { _customProperties = value; }
		}

		/// <summary>
		/// The location to a transformation to apply to a report data rendering.
		/// This can be a full folder path (e.g. "/xsl/xfrm.xsl"), relative path (e.g. "xfrm.xsl").
		/// </summary>
		public string DataTransform
		{
			get {return _dataTransform;}
			set {_dataTransform = value;}
		}

		/// <summary>
		/// The schema or namespace to use for a report data rendering.
		/// </summary>
		public string DataSchema
		{
			get {return _dataSchema;}
			set {_dataSchema = value;}
		}

		/// <summary>
		/// Name of the top level element that represents the report data.
		/// </summary>
		public string DataElementName
		{
			get {return _dataElementName;}
			set {_dataElementName = value;}
		}

		/// <summary>
		/// Indicates whether textboxes should render as elements or attributes.
		/// </summary>
		public ReportDataElementStyle DataElementStyle
		{
			get {return _dataElementStyle;}
			set {_dataElementStyle = value;}
		}

  
		#endregion

 
        #region IXmlSerializable Members

        /// <summary>
		/// Converts a Report into its RDL representation .
		/// </summary>
		/// <param name="writer">The <typeparamref name="XmlWriter"/> stream to which the Report is serialized</param>
        public virtual void WriteXml(XmlWriter writer)
		{
			//--- Write the root element attributes
			writer.WriteAttributeString("xmlns", RDL_SCHEMA_2005);
            writer.WriteAttributeString("xmlns:rd", RDL_DESIGNER);

			//--- ReportParameters
			if (_parameters != null)
				((IXmlSerializable)_parameters).WriteXml(writer);

			//--- DataSources
			if (_dataSources != null)
				((IXmlSerializable)_dataSources).WriteXml(writer);

			//--- Body
			if (_body != null)
				((IXmlSerializable)_body).WriteXml(writer);

			//--- DataSets
			if (_dataSets != null)
				((IXmlSerializable)_dataSets).WriteXml(writer);

			//--- LeftMargin
			if(_leftMargin.ToString() != "0in")
				writer.WriteElementString(Rdl.LEFTMARGIN, _leftMargin.ToString());

			//--- BottomMargin
			if (_bottomMargin.ToString() != "0in")
				writer.WriteElementString(Rdl.BOTTOMMARGIN, _bottomMargin.ToString());

			//--- RigthMargin
			if (_rightMargin.ToString() != "0in")
				writer.WriteElementString(Rdl.RIGHTMARGIN, _rightMargin.ToString());

			//--- TopMargin
			if (_topMargin.ToString() != "0in")
				writer.WriteElementString(Rdl.TOPMARGIN, _topMargin.ToString());

			//--- Description
			if (_description != null && _description != string.Empty)
				writer.WriteElementString(Rdl.DESCRIPTION, _description);

			//--- Author
			if (_author != null && _author != string.Empty)
				writer.WriteElementString(Rdl.AUTHOR, _author);

			//--- AutoRefresh
			if(_autoRefresh != 0)
				writer.WriteElementString(Rdl.AUTOREFRESH, _autoRefresh.ToString());

			//--- Code
			if(_code != null && _code != string.Empty)
				writer.WriteElementString(Rdl.CODE, _code);

			//--- Width
			writer.WriteElementString(Rdl.WIDTH, _width.ToString());

			//--- PageHeader
			if (_pageHeader != null)
				((IXmlSerializable)_pageHeader).WriteXml(writer);

			//--- PageFooter
			if (_pageFooter != null)
				((IXmlSerializable)_pageFooter).WriteXml(writer);

			//--- PageHeight
			writer.WriteElementString(Rdl.PAGEHEIGHT, _pageHeight.ToString());

			//--- PageWidth
    		writer.WriteElementString(Rdl.PAGEWIDTH, _pageWidth.ToString());

			//--- InteractiveHeight
			if (_interactiveHeight.ToString() != "11in")
				writer.WriteElementString(Rdl.INTERACTIVEHEIGHT, _interactiveHeight.ToString());

			//--- InteractiveWidth
			if (_interactiveWidth.ToString() != "8.5in")
				writer.WriteElementString(Rdl.INTERACTIVEWIDTH, _interactiveWidth.ToString());

			//--- EmbeddedImages
			if (_embeddedImages != null)
				((IXmlSerializable)_embeddedImages).WriteXml(writer);

			//--- Language
			if(_language != null && _language != string.Empty)
				writer.WriteElementString(Rdl.LANGUAGE, _language);


			//--- Custom
			if (_customProperties != null && _customProperties.Count > 0)
				((IXmlSerializable)_customProperties).WriteXml(writer);

			//--- DataTransform
			if(_dataTransform != null && _dataTransform != string.Empty)
				writer.WriteElementString(Rdl.DATATRANSFORM, _dataTransform);

			//--- DataSchema
			if (_dataSchema != null && _dataSchema != string.Empty)
				writer.WriteElementString(Rdl.DATASCHEMA, _dataSchema);

			//--- DataElementName
			if (_dataElementName != null && _dataElementName != string.Empty)
				writer.WriteElementString(Rdl.DATAELEMENTNAME, _dataElementName);

			//--- DataElementStyle
			if(_dataElementStyle != ReportDataElementStyle.AttributeNormal)
				writer.WriteElementString(Rdl.DATAELEMENTSTYLE, _dataElementStyle.ToString());
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			// TODO:  Add Report.GetSchema implementation
			return null;
		}

		/// <summary>
		/// Generates an Report from its RDL representation.
		/// </summary>
		/// <param name="reader">The <typeparamref name="XmlReader"/> stream from which the Report is deserialized</param>
		public virtual void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Rdl.REPORT)
				{
					break;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					//--- Description
					if (reader.Name == Rdl.DESCRIPTION)
						_description = reader.ReadString();

					//--- Author
					if (reader.Name == Rdl.AUTHOR)
						_author = reader.ReadString();

					//--- AutoRefresh
					if (reader.Name == Rdl.AUTOREFRESH)
						_autoRefresh = int.Parse(reader.ReadString());

					//--- DataSources
					if (reader.Name == Rdl.DATASOURCES && !reader.IsEmptyElement)
					{
						if (_dataSources == null)
							_dataSources = new DataSourceCollection();

						((IXmlSerializable)_dataSources).ReadXml(reader);
					}

					//--- DataSets
					if (reader.Name == Rdl.DATASETS && !reader.IsEmptyElement)
					{
						if (_dataSets == null)
							_dataSets = new DataSetCollection();

						((IXmlSerializable)_dataSets).ReadXml(reader);
					}

					//--- Body
					if (reader.Name == Rdl.BODY && !reader.IsEmptyElement)
					{
						if (_body == null)
							_body = new Body();

						((IXmlSerializable)_body).ReadXml(reader);
					}

					//--- Report Parameters
					if (reader.Name == Rdl.REPORTPARAMETERS && !reader.IsEmptyElement)
					{
						if (_parameters == null)
							_parameters = new ReportParameterCollection();

						((IXmlSerializable)_parameters).ReadXml(reader);
					}

					//--- Code
					if (reader.Name == Rdl.CODE)
						_code = reader.ReadString();

					//--- Width
					if (reader.Name == Rdl.WIDTH)
						_width = Size.Parse(reader.ReadString());

					//--- Page Header
					if (reader.Name == Rdl.PAGEHEADER && !reader.IsEmptyElement)
					{
						if (_pageHeader == null)
							_pageHeader = new PageHeader();

						((IXmlSerializable)_pageHeader).ReadXml(reader);
					}

					//--- Page Footer
					if (reader.Name == Rdl.PAGEFOOTER && !reader.IsEmptyElement)
					{
						if (_pageFooter == null)
							_pageFooter = new PageFooter();

						((IXmlSerializable)_pageFooter).ReadXml(reader);
					}

					//--- Page Height
					if (reader.Name == Rdl.PAGEHEIGHT)
						_pageHeight = Size.Parse(reader.ReadString());

					//--- Page Width
					if (reader.Name == Rdl.PAGEWIDTH)
						_pageWidth = Size.Parse(reader.ReadString());

					//--- Interactive Height
					if (reader.Name == Rdl.INTERACTIVEHEIGHT)
						_interactiveHeight = Size.Parse(reader.ReadString());

					//--- Interactive Width
					if (reader.Name == Rdl.INTERACTIVEWIDTH)
						_interactiveWidth = Size.Parse(reader.ReadString());

					//--- Left Margin
					if (reader.Name == Rdl.LEFTMARGIN)
						_leftMargin = Size.Parse(reader.ReadString());

					//--- Right Margin
					if (reader.Name == Rdl.RIGHTMARGIN)
						_rightMargin = Size.Parse(reader.ReadString());

					//--- Top Margin
					if (reader.Name == Rdl.TOPMARGIN)
						_topMargin = Size.Parse(reader.ReadString());

					//--- Bottom Margin
					if (reader.Name == Rdl.BOTTOMMARGIN)
						_bottomMargin = Size.Parse(reader.ReadString());

					//--- Embedded Images
					if (reader.Name == Rdl.EMBEDDEDIMAGES && !reader.IsEmptyElement)
					{
						if (_embeddedImages == null)
							_embeddedImages = new EmbeddedImageCollection();

						((IXmlSerializable)_embeddedImages).ReadXml(reader);
					}

					//--- Language
					if (reader.Name == Rdl.LANGUAGE)
						_language = reader.ReadString();


					//--- Custom Properties
					if (reader.Name == Rdl.CUSTOMPROPERTIES && !reader.IsEmptyElement)
					{
						if (_customProperties == null)
							_customProperties = new CustomPropertiesCollection();

						((IXmlSerializable)_customProperties).ReadXml(reader);
					}

					//--- Data Transform
					if (reader.Name == Rdl.DATATRANSFORM)
						_dataTransform = reader.ReadString();

					//--- Data Schema
					if (reader.Name == Rdl.DATASCHEMA)
						_dataSchema = reader.ReadString();

					//--- Data Element Name
					if (reader.Name == Rdl.DATAELEMENTNAME)
						_dataElementName = reader.ReadString();

					//--- Data Element Style
					if (reader.Name == Rdl.DATAELEMENTSTYLE)
						_dataElementStyle = (ReportDataElementStyle)Enum.Parse(typeof(ReportDataElementStyle), reader.ReadString());
				}
			}
		}

		#endregion

        #region Serialization
        /// <summary>
        /// Returns a byte array that contains the report definition for this instance.
        /// </summary>
        /// <returns>A byte array for this report.</returns>
        public virtual byte[] ToByteArray()
        {
            MemoryStream reportStream = new MemoryStream();

            XmlAttributes attributes = new XmlAttributes();
            attributes.XmlRoot = new XmlRootAttribute("Report");
            attributes.XmlRoot.Namespace = RDL_SCHEMA_2005;

            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            overrides.Add(typeof(ObjectModel.Report), attributes);

            //--- Serialize the report to XML
            XmlSerializer serializer = new XmlSerializer(typeof(Report), overrides);
            serializer.Serialize(reportStream, this);

            //--- Return the Byte array
            return reportStream.ToArray();
        }

        /// <summary>
        /// Serializes the report to a XmlDocument
        /// </summary>
        /// <returns>XmlDocument</returns>
        public virtual XmlDocument ToXmlDocument()
        {
            MemoryStream reportStream = new MemoryStream();

            //--- Serialize the report to XML
            XmlSerializer serializer = new XmlSerializer(typeof(Report), RDL_SCHEMA_2005);
            serializer.Serialize(reportStream, this);

            //--- Return XmlDocument
            reportStream.Position = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(reportStream);
            reportStream.Close();
            return doc;
            //return null;
        }
        #endregion

    }
}
