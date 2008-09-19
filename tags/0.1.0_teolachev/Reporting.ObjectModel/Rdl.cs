using System;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Summary description for Rdl.
	/// </summary>
	internal class Rdl
	{
		#region Constants

		#region Attribute Constants
 
		/// <summary>
		/// Name
		/// </summary>
		public const string NAME				= "Name";

		#endregion

		#region Common Constants

		/// <summary>
		/// Style
		/// </summary>
		public const string STYLE = "Style";

		/// <summary>
		/// Height
		/// </summary>
		public const string HEIGHT = "Height";

		/// <summary>
		/// Width
		/// </summary>
		public const string WIDTH = "Width";

		/// <summary>
		/// Custom
		/// </summary>
		public const string CUSTOM = "Custom";

		/// <summary>
		/// Value
		/// </summary>
		public const string VALUE = "Value";

		/// <summary>
		/// DataElementName
		/// </summary>
		public const string DATAELEMENTNAME = "DataElementName";

		/// <summary>
		/// DataElementStyle
		/// </summary>
		public const string DATAELEMENTSTYLE = "DataElementStyle";

		/// <summary>
		/// Filters
		/// </summary>
		public const string FILTERS = "Filters";

		/// <summary>
		/// ReportItems
		/// </summary>
		public const string REPORTITEMS = "ReportItems";

		/// <summary>
		/// Visibility
		/// </summary>
		public const string VISIBILITY = "Visibility";

		/// <summary>
		/// Nullable
		/// </summary>
		public const string NULLABLE = "Nullable";

		/// <summary>
		/// Prompt
		/// </summary>
		public const string PROMPT = "Prompt";

		#endregion

		#region Report Element

		/// <summary>
		/// Report
		/// </summary>
		public const string REPORT				= "Report";

		/// <summary>
		/// Description
		/// </summary>
		public const string DESCRIPTION			= "Description";

		/// <summary>
		/// Author
		/// </summary>
		public const string AUTHOR				= "Author";

		/// <summary>
		/// AutoRefresh
		/// </summary>
		public const string AUTOREFRESH			= "AutoRefresh";

		/// <summary>
		/// Datasources
		/// </summary>
		public const string DATASOURCES			= "DataSources";

		/// <summary>
		/// Datasource
		/// </summary>
		public const string DATASOURCE			= "DataSource";

		/// <summary>
		/// Datasets
		/// </summary>
		public const string DATASETS			= "DataSets";

		/// <summary>
		/// DataSet
		/// </summary>
		public const string DATASET				= "DataSet";

		/// <summary>
		/// Body
		/// </summary>
		public const string BODY				= "Body";

		/// <summary>
		/// ReportParameters
		/// </summary>
		public const string REPORTPARAMETERS	= "ReportParameters";

		/// <summary>
		/// Code
		/// </summary>
		public const string CODE				= "Code";

		/// <summary>
		/// PageHeader
		/// </summary>
		public const string PAGEHEADER			= "PageHeader";

		/// <summary>
		/// PageFooter
		/// </summary>
		public const string PAGEFOOTER			= "PageFooter";

		/// <summary>
		/// PageHeight
		/// </summary>
		public const string PAGEHEIGHT			= "PageHeight";

		/// <summary>
		/// PageWidth
		/// </summary>
		public const string PAGEWIDTH			= "PageWidth";

		/// <summary>
		/// InteractiveHeight
		/// </summary>
		public const string INTERACTIVEHEIGHT	= "InteractiveHeight";

		/// <summary>
		/// InteractiveWidth
		/// </summary>
		public const string INTERACTIVEWIDTH	 = "InteractiveWidth";

		/// <summary>
		/// LeftMargin
		/// </summary>
		public const string LEFTMARGIN			= "LeftMargin";

		/// <summary>
		/// RightMargin
		/// </summary>
		public const string RIGHTMARGIN			= "RightMargin";

		/// <summary>
		/// TopMargin
		/// </summary>
		public const string TOPMARGIN			= "TopMargin";

		/// <summary>
		/// BottomMargin
		/// </summary>
		public const string BOTTOMMARGIN		= "BottomMargin";

		/// <summary>
		/// EmbeddedImages
		/// </summary>
		public const string EMBEDDEDIMAGES		= "EmbeddedImages";

		/// <summary>
		/// Language
		/// </summary>
		public const string LANGUAGE			= "Language";

		/// <summary>
		/// CodeModules
		/// </summary>
		public const string CODEMODULES			= "CodeModules";

		/// <summary>
		/// Classes
		/// </summary>
		public const string CLASSES				= "Classes";

		/// <summary>
		/// CustomProperties
		/// </summary>
		public const string CUSTOMPROPERTIES	= "CustomProperties";

		/// <summary>
		/// CustomProperty
		/// </summary>
		public const string CUSTOMPROPERTY		= "CustomProperty";

		/// <summary>
		/// DataTransform
		/// </summary>
		public const string DATATRANSFORM		= "DataTransform";

		/// <summary>
		/// DataSchema
		/// </summary>
		public const string DATASCHEMA			= "DataSchema";

		#endregion

		#region ReportParameter Element

		/// <summary>
		/// ReportParameter
		/// </summary>
		public const string REPORTPARAMETER = "ReportParameter";

		/// <summary>
		/// DataType
		/// </summary>
		public const string DATATYPE = "DataType";

		/// <summary>
		/// DefaultValue
		/// </summary>
		public const string DEFAULTVALUE = "DefaultValue";

		/// <summary>
		/// AllowBlank
		/// </summary>
		public const string ALLOWBLANK = "AllowBlank";

		/// <summary>
		/// ValidValues
		/// </summary>
		public const string VALIDVALUES = "ValidValues";

		/// <summary>
		/// MultiValue
		/// </summary>
		public const string MULTIVALUE = "MultiValue";

		/// <summary>
		/// UsedInQuery
		/// </summary>
		public const string USEDINQUERY = "UsedInQuery";

		#endregion

		#region ConnectionProperties Element

		/// <summary>
		/// DataProvider
		/// </summary>
		public const string DATAPROVIDER = "DataProvider";

		/// <summary>
		/// ConnectString
		/// </summary>
		public const string CONNECTSTRING = "ConnectString";

		/// <summary>
		/// IntegratedSecurity
		/// </summary>
		public const string INTEGRATEDSECURITY = "IntegratedSecurity";

		#endregion

		#region Datasource Element

		/// <summary>
		/// Transaction
		/// </summary>
		public const string TRANSACTION = "Transaction";

		/// <summary>
		/// ConnectionProperties
		/// </summary>
		public const string CONNECTIONPROPERTIES = "ConnectionProperties";

		/// <summary>
		/// DataSourceReference
		/// </summary>
		public const string DATASOURCEREFERENCE = "DataSourceReference";

		

		#endregion

		#region DataSet Element

		/// <summary>
		/// Fields
		/// </summary>
		public const string FIELDS = "Fields";

		/// <summary>
		/// Field
		/// </summary>
		public const string FIELD = "Field";

		/// <summary>
		/// Query
		/// </summary>
		public const string QUERY = "Query";

		/// <summary>
		/// CaseSensitivity
		/// </summary>
		public const string CASESENSITIVITY = "CaseSensitivity";

		/// <summary>
		/// Collation
		/// </summary>
		public const string COLLATION = "Collation";

		/// <summary>
		/// AccentSensitivity
		/// </summary>
		public const string ACCENTSENSITIVITY = "AccentSensitivity";

		/// <summary>
		/// KanatypeSensitivity
		/// </summary>
		public const string KANATYPESENSITIVITY = "KanatypeSensitivity";

		/// <summary>
		/// WidthSensitivity
		/// </summary>
		public const string WIDTHSENSITIVITY = "WidthSensitivity";

		#endregion

		#region DefaultValue Element

		/// <summary>
		/// DataSetReference
		/// </summary>
		public const string DATASETREFERENCE = "DataSetReference";

		/// <summary>
		/// Values
		/// </summary>
		public const string VALUES = "Values";

		#endregion

		#region Field Element

		/// <summary>
		/// DataField
		/// </summary>
		public const string DATAFIELD = "DataField";
        public const string TYPENAME = "rd:TypeName";

		#endregion

		#region Query Element

		/// <summary>
		/// DataSourceName
		/// </summary>
		public const string DATASOURCENAME = "DataSourceName";
		
		/// <summary>
		/// CommandType
		/// </summary>
		public const string COMMANDTYPE = "CommandType";

		/// <summary>
		/// CommandText
		/// </summary>
		public const string COMMANDTEXT = "CommandText";

		/// <summary>
		/// QueryParameters
		/// </summary>
		public const string QUERYPARAMETERS = "QueryParameters";

		/// <summary>
		/// Timeout
		/// </summary>
		public const string TIMEOUT = "Timeout";

		#endregion

		#region QueryParameter Element

		/// <summary>
		/// QueryParameter
		/// </summary>
		public const string QUERYPARAMETER = "QueryParameter";

		#endregion

		#region Body Element

		/// <summary>
		/// Columns
		/// </summary>
		public const string COLUMNS = "Columns";

		/// <summary>
		/// ColumnSpacing
		/// </summary>
		public const string COLUMNSPACING = "ColumnSpacing";


		#endregion

		#region ReportItem Element

		/// <summary>
		/// TextBox
		/// </summary>
		public const string TEXTBOX = "Textbox";

		/// <summary>
		/// Line
		/// </summary>
		public const string LINE = "Line";

		/// <summary>
		/// Rectangle
		/// </summary>
		public const string RECTANGLE = "Rectangle";

		/// <summary>
		/// Image
		/// </summary>
		public const string IMAGE = "Image";

		/// <summary>
		/// Subreport
		/// </summary>
		public const string SUBREPORT = "Subreport";

		/// <summary>
		/// CustomReportItem
		/// </summary>
		public const string CUSTOMREPORTITEM = "CustomReportItem";

		/// <summary>
		/// Matrix
		/// </summary>
		public const string MATRIX = "Matrix";

		/// <summary>
		/// List
		/// </summary>
		public const string LIST = "List";

		/// <summary>
		/// Table
		/// </summary>
		public const string TABLE = "Table";

		/// <summary>
		/// Chart
		/// </summary>
		public const string CHART = "Chart";

		/// <summary>
		/// Action
		/// </summary>
		public const string ACTION = "Action";

		/// <summary>
		/// Top
		/// </summary>
		public const string TOP = "Top";

		/// <summary>
		/// Left
		/// </summary>
		public const string LEFT = "Left";

		/// <summary>
		/// ZIndex
		/// </summary>
		public const string ZINDEX = "ZIndex";

		/// <summary>
		/// ToolTip
		/// </summary>
		public const string TOOLTIP = "ToolTip";

		/// <summary>
		/// Label
		/// </summary>
		public const string LABEL = "Label";

		/// <summary>
		/// LinkToChild
		/// </summary>
		public const string LINKTOCHILD = "LinkToChild";

		/// <summary>
		/// Bookmark
		/// </summary>
		public const string BOOKMARK = "Bookmark";
		
		/// <summary>
		/// RepeatWith
		/// </summary>
		public const string REPEATWITH = "RepeatWith";

		/// <summary>
		/// DataElementOutput
		/// </summary>
		public const string DATAELEMENTOUTPUT = "DataElementOutput";

		#endregion

		#region DataRegion

		/// <summary>
		/// KeepTogether
		/// </summary>
		public const string KEEPTOGETHER = "KeepTogether";
		
		/// <summary>
		/// NoRows
		/// </summary>
		public const string NOROWS = "NoRows";

		/// <summary>
		/// DataSetName
		/// </summary>
		public const string DATASETNAME = "DataSetName";

		/// <summary>
		/// PageBreakAtStart
		/// </summary>
		public const string PAGEBREAKATSTART = "PageBreakAtStart";

		/// <summary>
		/// PageBreakAtEnd
		/// </summary>
		public const string PAGEBREAKATEND = "PageBreakAtEnd";

		#endregion

		#region Textbox Element

		/// <summary>
		/// CanGrow
		/// </summary>
		public const string CANGROW = "CanGrow";

		/// <summary>
		/// CanShrink
		/// </summary>
		public const string CANSHRINK = "CanShrink";

		/// <summary>
		/// HideDuplicates
		/// </summary>
		public const string HIDEDUPLICATES = "HideDuplicates";

		/// <summary>
		/// ToggleImage
		/// </summary>
		public const string TOGGLEIMAGE = "ToggleImage";

		#endregion

		#region Matrix Element

		/// <summary>
		/// Corner
		/// </summary>
		public const string CORNER = "Corner";

		/// <summary>
		/// ColumnGroupings
		/// </summary>
		public const string COLUMNGROUPINGS = "ColumnGroupings";

		/// <summary>
		/// RowGroupings
		/// </summary>
		public const string ROWGROUPINGS = "RowGroupings";

		/// <summary>
		/// MatrixRows
		/// </summary>
		public const string MATRIXROWS = "MatrixRows";

		/// <summary>
		/// MatrixColumns
		/// </summary>
		public const string MATRIXCOLUMNS = "MatrixColumns";

		/// <summary>
		/// LayoutDirection
		/// </summary>
		public const string LAYOUTDIRECTION = "LayoutDirection";

		/// <summary>
		/// GroupsBeforeRowHeaders
		/// </summary>
		public const string GROUPSBEFOREROWHEADERS = "GroupsBeforeRowHeaders";

		/// <summary>
		/// CellDataElementName
		/// </summary>
		public const string CELLDATAELEMENTNAME = "CellDataElementName";

		/// <summary>
		/// CellDataElementOutput
		/// </summary>
		public const string CELLDATAELEMENTOUTPUT = "CellDataElementOutput";

		#endregion

		#region ColumnGrouping Element

		/// <summary>
		/// ColumnGrouping
		/// </summary>
		public const string COLUMNGROUPING = "ColumnGrouping";

		/// <summary>
		/// DynamicColumns
		/// </summary>
		public const string DYNAMICCOLUMNS = "DynamicColumns";

		/// <summary>
		/// StaticColumns
		/// </summary>
		public const string STATICCOLUMNS = "StaticColumns";

		/// <summary>
		/// StaticColumn
		/// </summary>
		public const string STATICCOLUMN = "StaticColumn";

		#endregion

		#region RowGrouping Element

		/// <summary>
		/// RowGrouping
		/// </summary>
		public const string ROWGROUPING = "RowGrouping";

		/// <summary>
		/// DynamicRows
		/// </summary>
		public const string DYNAMICROWS = "DynamicRows";

		/// <summary>
		/// StaticRows
		/// </summary>
		public const string STATICROWS = "StaticRows";

		/// <summary>
		/// StaticRow
		/// </summary>
		public const string STATICROW = "StaticRow";

		#endregion

		#region DynamicColumns & DynamicRows Elements

		/// <summary>
		/// Grouping
		/// </summary>
		public const string GROUPING = "Grouping";

		/// <summary>
		/// Sorting
		/// </summary>
		public const string SORTING = "Sorting";

		/// <summary>
		/// Subtotal
		/// </summary>
		public const string SUBTOTAL = "Subtotal";

		#endregion

		#region Grouping Element

		/// <summary>
		/// GroupExpressions
		/// </summary>
		public const string GROUPEXPRESSIONS = "GroupExpressions";

		/// <summary>
		/// Parent
		/// </summary>
		public const string PARENT = "Parent";

		/// <summary>
		/// DataCollectionName
		/// </summary>
		public const string DATACOLLECTIONNAME = "DataCollectionName";
		
		#endregion

		#region SortBy Element

		/// <summary>
		/// SortBy
		/// </summary>
		public const string SORTBY = "SortBy";

		/// <summary>
		/// Direction
		/// </summary>
		public const string DIRECTION = "Direction";

		#endregion

		#region GroupExpression Element

		/// <summary>
		/// GroupExpression
		/// </summary>
		public const string GROUPEXPRESSION = "GroupExpression";		

		#endregion

		#region Subtotal Element

		/// <summary>
		/// Position
		/// </summary>
		public const string POSITION = "Position";

		#endregion

		#region Visibility Element

		/// <summary>
		/// Hidden
		/// </summary>
		public const string HIDDEN = "Hidden";

		/// <summary>
		/// ToggleItem
		/// </summary>
		public const string TOGGLEITEM = "ToggleItem";


		#endregion

		#region CodeModule Element

		/// <summary>
		/// CodeModule
		/// </summary>
		public const string CODEMODULE = "CodeModule";


		#endregion

		#region ParameterValues Element

		/// <summary>
		/// ParameterValues
		/// </summary>
		public const string PARAMETERVALUES = "ParameterValues";

		/// <summary>
		/// ParameterValue
		/// </summary>
		public const string PARAMETERVALUE = "ParameterValue";


		#endregion

		#region DataSetReference Element

		/// <summary>
		/// ValueField
		/// </summary>
		public const string VALUEFIELD = "ValueField";

		/// <summary>
		/// LabelField
		/// </summary>
		public const string LABELFIELD = "LabelField";


		#endregion

		#region Filter Element

		/// <summary>
		/// Filter
		/// </summary>
		public const string FILTER = "Filter";

		/// <summary>
		/// FilterExpression
		/// </summary>
		public const string FILTEREXPRESSION = "FilterExpression";

		/// <summary>
		/// Operator
		/// </summary>
		public const string OPERATOR = "Operator";

		/// <summary>
		/// FilterValues
		/// </summary>
		public const string FILTERVALUES = "FilterValues";

		/// <summary>
		/// FilterValue
		/// </summary>
		public const string FILTERVALUE = "FilterValue";

		#endregion

		#region Class Element

		/// <summary>
		/// Class
		/// </summary>
		public const string CLASS = "Class";

		/// <summary>
		/// ClassName
		/// </summary>
		public const string CLASSNAME = "ClassName";

		/// <summary>
		/// InstanceName
		/// </summary>
		public const string INSTANCENAME = "InstanceName";

		#endregion

		#region PageHeader & PageFooter Elements

		/// <summary>
		/// PrintOnFirstPage
		/// </summary>
		public const string PRINTONFIRSTPAGE = "PrintOnFirstPage";

		/// <summary>
		/// PrintOnLastPage
		/// </summary>
		public const string PRINTONLASTPAGE = "PrintOnLastPage";

		#endregion

		#region EmbeddedImage Element

		/// <summary>
		/// EmbeddedImage
		/// </summary>
		public const string EMBEDDEDIMAGE = "EmbeddedImage";

		/// <summary>
		/// MIMEType
		/// </summary>
		public const string MIMETYPE = "MIMEType";

		/// <summary>
		/// ImageData
		/// </summary>
		public const string IMAGEDATA = "ImageData";

		#endregion

		#region Action Element

		/// <summary>
		/// Hyperlink
		/// </summary>
		public const string HYPERLINK = "Hyperlink";

		/// <summary>
		/// Drillthrough
		/// </summary>
		public const string DRILLTHROUGH = "Drillthrough";

		/// <summary>
		/// BookmarkLink
		/// </summary>
		public const string BOOKMARKLINK = "BookmarkLink";

		#endregion

		#region Drillthrough Element

		/// <summary>
		/// ReportName
		/// </summary>
		public const string REPORTNAME = "ReportName";

		/// <summary>
		/// Parameters
		/// </summary>
		public const string PARAMETERS = "Parameters";

		#endregion

		#region ToggleImage Element

		/// <summary>
		/// InitialState
		/// </summary>
		public const string INITIALSTATE = "InitialState";

		#endregion

		#region Image Element

		/// <summary>
		/// Source
		/// </summary>
		public const string SOURCE = "Source";

		/// <summary>
		/// Sizing
		/// </summary>
		public const string SIZING = "Sizing";


		#endregion

		#region UserSort Element

		/// <summary>
		/// UserSort
		/// </summary>
		public const string USERSORT = "UserSort";

		/// <summary>
		/// SortExpression
		/// </summary>
		public const string SORTEXPRESSION = "SortExpression";

		/// <summary>
		/// SortExpressionScope
		/// </summary>
		public const string SORTEXPRESSIONSCOPE = "SortExpressionScope";

		/// <summary>
		/// SortTarget
		/// </summary>
		public const string SORTTARGET = "SortTarget";

		#endregion

		#region Image Element

		/// <summary>
		/// AltReportItem
		/// </summary>
		public const string ALTREPORTITEM = "AltReportItem";

		/// <summary>
		/// CustomData
		/// </summary>
		public const string CUSTOMDATA = "CustomData";

		#endregion

		#region Subreport Element

		/// <summary>
		/// MergeTransactions
		/// </summary>
		public const string MERGETRANSACTIONS = "MergeTransactions";


		#endregion

		#region CustomData Element

		/// <summary>
		/// DataColumnGroupings
		/// </summary>
		public const string DATACOLUMNGROUPINGS = "DataColumnGroupings";

		/// <summary>
		/// DataRowGroupings
		/// </summary>
		public const string DATAROWGROUPINGS = "DataRowGroupings";

		/// <summary>
		/// DataRows
		/// </summary>
		public const string DATAROWS = "DataRows";


		#endregion

		#region Parameter Element

		/// <summary>
		/// Parameter
		/// </summary>
		public const string PARAMETER = "Parameter";

		/// <summary>
		/// Omit
		/// </summary>
		public const string OMIT = "Omit";

		#endregion

		#region Table Element

		/// <summary>
		/// TableColumns
		/// </summary>
		public const string TABLECOLUMNS = "TableColumns";

		/// <summary>
		/// Header
		/// </summary>
		public const string HEADER = "Header";

		/// <summary>
		/// TableGroups
		/// </summary>
		public const string TABLEGROUPS = "TableGroups";

		/// <summary>
		/// Details
		/// </summary>
		public const string DETAILS = "Details";

		/// <summary>
		/// Footer
		/// </summary>
		public const string FOOTER = "Footer";

		/// <summary>
		/// FillPage
		/// </summary>
		public const string FILLPAGE = "FillPage";

		/// <summary>
		/// DetailDataElementName
		/// </summary>
		public const string DETAILDATAELEMENTNAME = "DetailDataElementName";

		/// <summary>
		/// DetailDataCollectionName
		/// </summary>
		public const string DETAILDATACOLLECTIONNAME = "DetailDataCollectionName";

		/// <summary>
		/// DetailDataElementOutput
		/// </summary>
		public const string DETAILDATAELEMENTOUTPUT = "DetailDataElementOutput";


		#endregion

		#region TableColumn Element

		/// <summary>
		/// TableColumn
		/// </summary>
		public const string TABLECOLUMN = "TableColumn";

		/// <summary>
		/// FixedHeader
		/// </summary>
		public const string FIXEDHEADER = "FixedHeader";

		#endregion

		#region Header Element

		/// <summary>
		/// TableRows
		/// </summary>
		public const string TABLEROWS = "TableRows";

		/// <summary>
		/// RepeatOnNewPage
		/// </summary>
		public const string REPEATONNEWPAGE = "RepeatOnNewPage";

		#endregion

		#region TableRow Element

		/// <summary>
		/// TableRow
		/// </summary>
		public const string TABLEROW = "TableRow";

		/// <summary>
		/// TableCells
		/// </summary>
		public const string TABLECELLS = "TableCells";

		#endregion

		#region TableGroup Element

		/// <summary>
		/// TableGroup
		/// </summary>
		public const string TABLEGROUP = "TableGroup";

		#endregion

		#region TableCell Element

		/// <summary>
		/// TableCell
		/// </summary>
		public const string TABLECELL = "TableCell";

		/// <summary>
		/// ColSpan
		/// </summary>
		public const string COLSPAN = "ColSpan";

		#endregion

		#region Style Element

		/// <summary>
		/// BorderColor
		/// </summary>
		public const string BORDERCOLOR = "BorderColor";

		/// <summary>
		/// BorderStyle
		/// </summary>
		public const string BORDERSTYLE = "BorderStyle";

		/// <summary>
		/// BorderWidth
		/// </summary>
		public const string BORDERWIDTH = "BorderWidth";

		/// <summary>
		/// BackgroundColor
		/// </summary>
		public const string BACKGROUNDCOLOR = "BackgroundColor";

		/// <summary>
		/// BackgroundGradientType
		/// </summary>
		public const string BACKGROUNDGRADIENTTYPE = "BackgroundGradientType";

		/// <summary>
		/// BackgroundGradientEndColor
		/// </summary>
		public const string BACKGROUNDGRADIENTENDCOLOR = "BackgroundGradientEndColor";

		/// <summary>
		/// BackgroundImage
		/// </summary>
		public const string BACKGROUNDIMAGE = "BackgroundImage";

		/// <summary>
		/// FontStyle
		/// </summary>
		public const string FONTSTYLE = "FontStyle";

		/// <summary>
		/// FontFamily
		/// </summary>
		public const string FONTFAMILY = "FontFamily";

		/// <summary>
		/// FontSize
		/// </summary>
		public const string FONTSIZE = "FontSize";

		/// <summary>
		/// FontWeight
		/// </summary>
		public const string FONTWEIGHT = "FontWeight";

		/// <summary>
		/// Format
		/// </summary>
		public const string FORMAT = "Format";

		/// <summary>
		/// TextDecoration
		/// </summary>
		public const string TEXTDECORATION = "TextDecoration";

		/// <summary>
		/// TextAlign
		/// </summary>
		public const string TEXTALIGN = "TextAlign";

		/// <summary>
		/// VerticalAlign
		/// </summary>
		public const string VERTICALALIGN = "VerticalAlign";

		/// <summary>
		/// Color
		/// </summary>
		public const string COLOR = "Color";

		/// <summary>
		/// PaddingLeft
		/// </summary>
		public const string PADDINGLEFT = "PaddingLeft";

		/// <summary>
		/// PaddingRight
		/// </summary>
		public const string PADDINGRIGHT = "PaddingRight";

		/// <summary>
		/// PaddingTop
		/// </summary>
		public const string PADDINGTOP = "PaddingTop";

		/// <summary>
		/// PaddingBottom
		/// </summary>
		public const string PADDINGBOTTOM = "PaddingBottom";

		/// <summary>
		/// LineHeight
		/// </summary>
		public const string LINEHEIGHT = "LineHeight";

		/// <summary>
		/// WritingMode
		/// </summary>
		public const string WRITINGMODE = "WritingMode";

		/// <summary>
		/// UnicodeBiDi
		/// </summary>
		public const string UNICODEBIDI = "UnicodeBiDi";

		/// <summary>
		/// Calendar
		/// </summary>
		public const string CALENDAR = "Calendar";

		/// <summary>
		/// NumeralLanguage
		/// </summary>
		public const string NUMERALLANGUAGE = "NumeralLanguage";

		/// <summary>
		/// NumeralVariant
		/// </summary>
		public const string NUMERALVARIANT = "NumeralVariant";


		#endregion

		#region BorderColor Element

		/// <summary>
		/// Default
		/// </summary>
		public const string DEFAULT = "Default";

		/// <summary>
		/// Right
		/// </summary>
		public const string RIGHT = "Right";

		/// <summary>
		/// Bottom
		/// </summary>
		public const string BOTTOM = "Bottom";

		#endregion

		#region BackgroundImage Element

		/// <summary>
		/// BackgroundRepeat
		/// </summary>
		public const string BACKGROUNDREPEAT = "BackgroundRepeat";

		#endregion

		#region List Element

		/// <summary>
		/// DataInstanceName
		/// </summary>
		public const string DATAINSTANCENAME = "DataInstanceName";

		/// <summary>
		/// DataInstanceElementOutput
		/// </summary>
		public const string DATAINSTANCEELEMENTOUTPUT = "DataInstanceElementOutput";

		#endregion

		#region MatrixColumn Element

		/// <summary>
		/// MatrixColumn
		/// </summary>
		public const string MATRIXCOLUMN = "MatrixColumn";

		#endregion

		#region MatrixRow Element

		/// <summary>
		/// MatrixRow
		/// </summary>
		public const string MATRIXROW = "MatrixRow";

		#endregion

		#region MatrixCells Element

		/// <summary>
		/// MatrixCells
		/// </summary>
		public const string MATRIXCELLS = "MatrixCells";

		/// <summary>
		/// MatrixCell
		/// </summary>
		public const string MATRIXCELL = "MatrixCell";

		#endregion

		#region Chart Element

		/// <summary>
		/// Type
		/// </summary>
		public const string TYPE = "Type";

		/// <summary>
		/// Subtype
		/// </summary>
		public const string SUBTYPE = "Subtype";

		/// <summary>
		/// SeriesGroupings
		/// </summary>
		public const string SERIESGROUPINGS = "SeriesGroupings";

		/// <summary>
		/// CategoryGroupings
		/// </summary>
		public const string CATEGORYGROUPINGS = "CategoryGroupings";

		/// <summary>
		/// ChartData
		/// </summary>
		public const string CHARTDATA = "ChartData";

		/// <summary>
		/// Legend
		/// </summary>
		public const string LEGEND = "Legend";

		/// <summary>
		/// CategoryAxis
		/// </summary>
		public const string CATEGORYAXIS = "CategoryAxis";

		/// <summary>
		/// ValueAxis
		/// </summary>
		public const string VALUEAXIS = "ValueAxis";

		/// <summary>
		/// Title
		/// </summary>
		public const string TITLE = "Title";

		/// <summary>
		/// PointWidth
		/// </summary>
		public const string POINTWIDTH = "PointWidth";

		/// <summary>
		/// Palette
		/// </summary>
		public const string PALETTE = "Palette";

		/// <summary>
		/// ThreeDProperties
		/// </summary>
		public const string THREEDPROPERTIES = "ThreeDProperties";

		/// <summary>
		/// PlotArea
		/// </summary>
		public const string PLOTAREA = "PlotArea";

		/// <summary>
		/// ChartElementOutput
		/// </summary>
		public const string CHARTELEMENTOUTPUT = "ChartElementOutput";

		#endregion

		#region SeriesGrouping Element

		/// <summary>
		/// SeriesGrouping
		/// </summary>
		public const string SERIESGROUPING = "SeriesGrouping";

		/// <summary>
		/// DynamicSeries
		/// </summary>
		public const string DYNAMICSERIES = "DynamicSeries";

		/// <summary>
		/// StaticSeries
		/// </summary>
		public const string STATICSERIES = "StaticSeries";

		#endregion

		#region StaticSeries Element

		/// <summary>
		/// StaticMember
		/// </summary>
		public const string STATICMEMBER = "StaticMember";

		#endregion

		#region CategoryGrouping Element

		/// <summary>
		/// CategoryGrouping
		/// </summary>
		public const string CATEGORYGROUPING = "CategoryGrouping";

		/// <summary>
		/// DynamicCategories
		/// </summary>
		public const string DYNAMICCATEGORIES = "DynamicCategories";

		/// <summary>
		/// StaticCategories
		/// </summary>
		public const string STATICCATEGORIES = "StaticCategories";

		#endregion

		#region Title Element

		/// <summary>
		/// Caption
		/// </summary>
		public const string CAPTION = "Caption";

		#endregion

		#region Legend Element

		/// <summary>
		/// Visible
		/// </summary>
		public const string VISIBLE = "Visible";

		/// <summary>
		/// InsidePlotArea
		/// </summary>
		public const string INSIDEPLOTAREA = "InsidePlotArea";

		/// <summary>
		/// Layout
		/// </summary>
		public const string LAYOUT = "Layout";
		
		#endregion

		#region CategoryAxis & ValueAxis Element

		/// <summary>
		/// Axis
		/// </summary>
		public const string AXIS = "Axis";

		#endregion

		#region Axis Element

		/// <summary>
		/// Margin
		/// </summary>
		public const string MARGIN = "Margin";

		/// <summary>
		/// MajorTickMarks
		/// </summary>
		public const string MAJORTICKMARKS = "MajorTickMarks";

		/// <summary>
		/// MinorTickMarks
		/// </summary>
		public const string MINORTICKMARKS = "MinorTickMarks";

		/// <summary>
		/// MajorGridLines
		/// </summary>
		public const string MAJORGRIDLINES = "MajorGridLines";

		/// <summary>
		/// MinorGridLines
		/// </summary>
		public const string MINORGRIDLINES = "MinorGridLines";

		/// <summary>
		/// MajorInterval
		/// </summary>
		public const string MAJORINTERVAL = "MajorInterval";

		/// <summary>
		/// MinorInterval
		/// </summary>
		public const string MINORINTERVAL = "MinorInterval";

		/// <summary>
		/// Reverse
		/// </summary>
		public const string REVERSE = "Reverse";

		/// <summary>
		/// CrossAt
		/// </summary>
		public const string CROSSAT = "CrossAt";

		/// <summary>
		/// Interlaced
		/// </summary>
		public const string INTERLACED = "Interlaced";

		/// <summary>
		/// Scalar
		/// </summary>
		public const string SCALAR = "Scalar";

		/// <summary>
		/// Min
		/// </summary>
		public const string MIN = "Min";

		/// <summary>
		/// Max
		/// </summary>
		public const string MAX = "Max";

		/// <summary>
		/// LogScale
		/// </summary>
		public const string LOGSCALE = "LogScale";

		#endregion

		#region ChartData Element

		/// <summary>
		/// ChartSeries
		/// </summary>
		public const string CHARTSERIES = "ChartSeries";

		#endregion

		#region ChartSeries Element

		/// <summary>
		/// DataPoints
		/// </summary>
		public const string DATAPOINTS = "DataPoints";

		/// <summary>
		/// PlotType
		/// </summary>
		public const string PLOTTYPE = "PlotType";

		#endregion

		#region DataPoint Element

		/// <summary>
		/// DataPoint
		/// </summary>
		public const string DATAPOINT = "DataPoint";

		/// <summary>
		/// DataValues
		/// </summary>
		public const string DATAVALUES = "DataValues";

		/// <summary>
		/// DataLabel
		/// </summary>
		public const string DATALABEL = "DataLabel";

		/// <summary>
		/// Marker
		/// </summary>
		public const string MARKER = "Marker";

		#endregion

		#region DataPoint Element

		/// <summary>
		/// DataValue
		/// </summary>
		public const string DATAVALUE = "DataValue";

		#endregion

		#region DataLabel Element

		/// <summary>
		/// Rotation
		/// </summary>
		public const string ROTATION = "Rotation";

		#endregion

		#region Marker Element

		/// <summary>
		/// Size
		/// </summary>
		public const string SIZE = "Size";

		#endregion

		#region MajorGridLines Element

		/// <summary>
		/// ShowGridLines
		/// </summary>
		public const string SHOWGRIDLINES = "ShowGridLines";

		#endregion

		#region ThreeDProperties Element

		/// <summary>
		/// Enabled
		/// </summary>
		public const string ENABLED = "Enabled";

		/// <summary>
		/// ProjectionMode
		/// </summary>
		public const string PROJECTIONMODE = "ProjectionMode";

		/// <summary>
		/// Inclination
		/// </summary>
		public const string INCLINATION = "Inclination";

		/// <summary>
		/// Perspective
		/// </summary>
		public const string PERSPECTIVE = "Perspective";

		/// <summary>
		/// HeightRatio
		/// </summary>
		public const string HEIGHTRATIO = "HeightRatio";

		/// <summary>
		/// DepthRatio
		/// </summary>
		public const string DEPTHRATIO = "DepthRatio";

		/// <summary>
		/// Shading
		/// </summary>
		public const string SHADING = "Shading";

		/// <summary>
		/// GapDepth
		/// </summary>
		public const string GAPDEPTH = "GapDepth";

		/// <summary>
		/// WallThickness
		/// </summary>
		public const string WALLTHICKNESS = "WallThickness";

		/// <summary>
		/// DrawingStyle
		/// </summary>
		public const string DRAWINGSTYLE = "DrawingStyle";

		/// <summary>
		/// Clustered
		/// </summary>
		public const string CLUSTERED = "Clustered";

		#endregion

		#endregion

		static Rdl(){}
	}
}
