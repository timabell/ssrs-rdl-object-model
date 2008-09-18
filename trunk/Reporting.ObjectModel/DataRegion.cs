using System;
using System.Xml;
using System.Xml.Serialization;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Represents the layout of a data set in a report.
	/// </summary>
	public class DataRegion : ReportItem
	{
		#region Private Variables

		private bool				_keepTogether;
		private Expression			_noRows;
		private string				_dataSetName;
		private bool				_pageBreakAtStart;
		private bool				_pageBreakAtEnd;
		private	FilterCollection	_filters;

		#endregion

		/// <summary>
		/// Creates an instance of a DataRegion class.
		/// </summary>
		public DataRegion(){}

		#region Public Properties

		/// <summary>
		/// Indicates the entire data region should be kept together 
		/// on one page if possible.
		/// </summary>
		public bool KeepTogther
		{
			get {return _keepTogether;}
			set {_keepTogether = value;}
		}

		/// <summary>
		/// Message to diplay in the DataRegion when no rows of data are available.
		/// </summary>
		public Expression NoRows
		{
			get {return _noRows;}
			set {_noRows = value;}
		}

		/// <summary>
		/// Indicates which data set to use for this data region.
		/// </summary>
		public string DataSetName
		{
			get {return _dataSetName;}
			set { _dataSetName = value; }
		}

		/// <summary>
		/// Indicates the report should page break at the start of the data region.
		/// </summary>
		public bool PageBreakAtStart
		{
			get {return _pageBreakAtStart;}
			set {_pageBreakAtStart = value;}
		}

		/// <summary>
		/// Indicates the report should page break at the end of the data region.
		/// </summary>
		public bool PageBreakAtEnd
		{
			get {return _pageBreakAtEnd;}
			set {_pageBreakAtEnd = value;}
		}

		/// <summary>
		/// Filters to apply to each row of data in the data region.
		/// </summary>
		public FilterCollection Filters
		{
			get {return _filters;}
			set {_filters = value;}
		}

		#endregion

		#region Protected Methods

		protected override string GetRootElement()
		{
			return string.Empty;
		}

		protected override void WriteRDL(XmlWriter writer)
		{
			//--- KeepTogether
			if(_keepTogether)
				writer.WriteElementString(Rdl.KEEPTOGETHER, _keepTogether.ToString().ToLower());

			//--- NoRows
			if (_noRows != null)
				writer.WriteElementString(Rdl.NOROWS, _noRows.Value.ToString());

			//--- DataSetName
			if (_dataSetName != null && _dataSetName != string.Empty)
				writer.WriteElementString(Rdl.DATASETNAME, _dataSetName.ToString());

			//--- PageBreakAtStart
			if (_pageBreakAtStart)
				writer.WriteElementString(Rdl.PAGEBREAKATSTART, _pageBreakAtStart.ToString().ToLower());

			//--- PageBreakAtEnd
			if (_pageBreakAtEnd)
				writer.WriteElementString(Rdl.PAGEBREAKATEND, _pageBreakAtEnd.ToString().ToLower());

			//--- Filters
			if (_filters != null)
				((IXmlSerializable)_filters).WriteXml(writer);
		}

		protected override void ReadRDL(XmlReader reader)
		{
			//--- KeepTogether
			if (reader.Name == Rdl.KEEPTOGETHER)
				_keepTogether = bool.Parse(reader.ReadString());

			//--- NoRows
			if (reader.Name == Rdl.NOROWS)
			{
				if (_noRows == null)
					_noRows = new Expression();

				_noRows.Value = reader.ReadString();
			}

			//--- DataSetName
			if (reader.Name == Rdl.DATASETNAME)
				_dataSetName = reader.ReadString();

			//--- PageBreakAtStart
			if (reader.Name == Rdl.PAGEBREAKATSTART)
				_pageBreakAtStart = bool.Parse(reader.ReadString());

			//--- PageBreakAtEnd
			if (reader.Name == Rdl.PAGEBREAKATEND)
				_pageBreakAtEnd = bool.Parse(reader.ReadString());

			//--- Filters
			if (reader.Name == Rdl.FILTERS && !reader.IsEmptyElement)
			{
				if (_filters == null)
					_filters = new FilterCollection();

				((IXmlSerializable)_filters).ReadXml(reader);
			}
		}

		#endregion
	}
}
