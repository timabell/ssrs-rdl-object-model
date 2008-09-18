using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Reporting.ObjectModel
{
	/// <summary>
	/// Defines a tabular grouped layout of the data region.
	/// </summary>
	public class Table : DataRegion
    {
        #region Constants
        internal const string MAIN_TABLE_NAME = "tblMain";
        #endregion

        #region Private Variables

        private TableColumnCollection	_tableColumns;
		private Header					_header;
		private TableGroupCollection	_tableGroups;
		private Details					_details;
		private Footer					_footer;
		private bool					_fillPage;
		private string					_detailDataElementName;
		private string					_detailDataCollectionName;
		private DetailDataElementOutput _detailDataElementOutput;
        private int                     _originalColumnCount;
        private Report _report;

		#endregion

		public Table()
        {
            this.Header = new Header();
            this.TableGroups = new TableGroupCollection();
            this.Details = new Details();
            this.Footer = new Footer();
            this.TableColumns = new TableColumnCollection();
            this.Header.RepeatOnNewPage = true;
        }
        public Table(string name): this()
        {
            this._name = name;
        }
        public Table(Report report):this()
        {
            _report = report;
        }

		/// <summary>
		/// The columns in that table.
		/// </summary>
		public TableColumnCollection TableColumns
		{
			get {return _tableColumns;}
			set {_tableColumns = value;}
		}

		/// <summary>
		/// The table header rows.
		/// </summary>
		public Header Header
		{
			get {return _header;}
			set {_header = value;}
		}

		/// <summary>
		/// The groups for the table.
		/// </summary>
		public TableGroupCollection TableGroups
		{
			get {return _tableGroups;}
			set {_tableGroups = value;}
		}

		/// <summary>
		/// The details rows for the table.
		/// </summary>
		public Details Details
		{
			get {return _details;}
			set {_details = value;}
		}

		/// <summary>
		/// The table footer rows.
		/// </summary>
		public Footer Footer
		{
			get {return _footer;}
			set {_footer = value;}
		}

		/// <summary>
		/// Inidicates the table should expand to fill the page, pushing 
		/// items below it to the bottom of the page.
		/// </summary>
		public bool FillPage
		{
			get {return _fillPage;}
			set {_fillPage = value;}
		}

		/// <summary>
		/// The name to use for the data element for instances of this group.
		/// </summary>
		[DefaultValue("Details")]
		public string DetailDataElementName
		{
			get {return _detailDataElementName;}
			set {_detailDataElementName = value;}
		}

		/// <summary>
		/// The name to use for the data element for the collection of all instances of this group.
		/// </summary>
		[DefaultValue("Details_Collection")]
		public string DetailDataCollectionName
		{
			get {return _detailDataCollectionName;}
			set {_detailDataCollectionName = value;}
		}

		/// <summary>
		/// Indicates whether the details should appear in a data redering.
		/// </summary>
		public DetailDataElementOutput DetailDataElementOutput
		{
			get {return _detailDataElementOutput;}
			set {_detailDataElementOutput = value;}
		}


        /// <summary>
        /// Holds a reference to the table container
        /// </summary>
        private int OriginalColumnCount
        {
            get { return _originalColumnCount; }
            set { _originalColumnCount = value; }
        }

		#region Hidden Properties

		private new Size Width
		{
			get {return base.Width;}
			set {base.Width = value;}
		}

		private new string LinkToChild
		{
			get {return base.LinkToChild;}
			set{base.LinkToChild = value;}
		}

		private new string RepeatWith
		{
			get {return base.RepeatWith;}
			set{base.RepeatWith = value;}
		}

		#endregion

		#region Protected Methods

		protected override string GetRootElement()
		{
			return Rdl.TABLE;
		}

        public override Size Height
        {
            get
            {
                Size height = new Size(0);

                if (_header != null) height += _header.Height;
                if (_tableGroups != null) height += _tableGroups.Height;
                if (_details != null) height += _details.Height;
                if (_footer != null) height += _footer.Height;
                return height;
            }

        }
		protected override void ReadRDL(XmlReader reader)
		{
			base.ReadRDL(reader);

			//--- TableColumns
			if (reader.Name == Rdl.TABLECOLUMNS)
			{
				if (_tableColumns == null)
					_tableColumns = new TableColumnCollection();

				((IXmlSerializable)_tableColumns).ReadXml(reader);
			}

			//--- Header
			if (reader.Name == Rdl.HEADER)
			{
				if (_header == null)
					_header = new Header();

				((IXmlSerializable)_header).ReadXml(reader);
			}

			//--- TableGroups
			if (reader.Name == Rdl.TABLEGROUPS)
			{
				if (_tableGroups == null)
					_tableGroups = new TableGroupCollection();

				((IXmlSerializable)_tableGroups).ReadXml(reader);
			}

			//--- Details
			if (reader.Name == Rdl.DETAILS)
			{
				if (_details == null)
					_details = new Details();

				((IXmlSerializable)_details).ReadXml(reader);
			}

			//--- Footer
			if (reader.Name == Rdl.FOOTER)
			{
				if (_footer == null)
					_footer = new Footer();

				((IXmlSerializable)_footer).ReadXml(reader);
			}

			//--- FillPage
			if (reader.Name == Rdl.FILLPAGE)
				_fillPage = bool.Parse(reader.ReadString());

			//--- DetailDataElementName
			if (reader.Name == Rdl.DETAILDATAELEMENTNAME)
				_detailDataElementName = reader.ReadString();

			//--- DetailDataCollectionName
			if (reader.Name == Rdl.DETAILDATACOLLECTIONNAME)
				_detailDataCollectionName = reader.ReadString();

			//--- DetailDataElementOutput
			if (reader.Name == Rdl.DETAILDATAELEMENTOUTPUT)
				_detailDataElementOutput = (DetailDataElementOutput)Enum.Parse(typeof(DetailDataElementOutput), reader.ReadString());
		}

		protected override void WriteRDL(XmlWriter writer)
		{
			//--- TableGroups
			if (_tableGroups != null && _tableGroups.Count > 0)
				((IXmlSerializable)_tableGroups).WriteXml(writer);

			//--- Details
			if (_details != null)
				((IXmlSerializable)_details).WriteXml(writer);

			//--- TableColumns
			if (_tableColumns != null)
				((IXmlSerializable)_tableColumns).WriteXml(writer);

			//--- Header
			if (_header != null)
				((IXmlSerializable)_header).WriteXml(writer);

			//--- Footer
			if (_footer != null)
				((IXmlSerializable)_footer).WriteXml(writer);

			//--- FillPage
			if(_fillPage)
				writer.WriteElementString(Rdl.FILLPAGE, _fillPage.ToString().ToLower());

			//--- DetailDataElementName
			if (_detailDataElementName != null && _detailDataElementName != string.Empty)
				writer.WriteElementString(Rdl.DETAILDATAELEMENTNAME, _detailDataElementName);

			//--- DetailDataCollectionName
			if (_detailDataCollectionName != null && _detailDataCollectionName != string.Empty)
				writer.WriteElementString(Rdl.DETAILDATACOLLECTIONNAME, _detailDataCollectionName);

			//--- DetailDataElementOutput
			if(_detailDataElementOutput != DetailDataElementOutput.Output)
				writer.WriteElementString(Rdl.DETAILDATAELEMENTOUTPUT, _detailDataElementOutput.ToString());

			base.WriteRDL(writer);
		}

		#endregion

        //#region Table Report Generation
        ///// <summary>
        ///// Generates a table region
        ///// </summary>
        ///// <param name="parent"></param>
        //internal void GenerateReport()
        //{
        //    RemoveHiddenColumns();


        //    // Crosstab reporting
        //    if (_report.IsCrosstabReport)
        //    {
        //        Debug.Assert(_report.Entities!=null && _report.Entities.Count > 0, Resources.NO_CROSSTAB_ITEMS);
        //        if (_report.Entities == null || _report.Entities.Count == 0) throw new ApplicationException(Resources.NO_CROSSTAB_ITEMS);

        //        // store the original count of the measure columns to generate the headers
        //        this.OriginalColumnCount = _report.Columns.GetCount(typeof(MeasureColumn));
        //        // sort the columns by moving the identity columns to the left
        //        _report.Columns.Sort();
        //        // generate the crosstab columns
        //        _report.Columns.GenerateCrosstabColumns(_report.Entities, _report.IsItemByCrosstabReport);
        //    }

        //    GenerateTableColumns();
        //    GenerateTableHeader();
        //    GenerateTableGroups();
        //    GenerateTableDetails();
        //    // Generate the grand total if any
        //    GrandTotal grand = _report.ReportGroups.GrandTotal;
        //    if (grand != null && grand.Visible.ToLower()!= "false") GenerateTableFooter(grand);
            
        //}
        ///// <summary>
        ///// Remove hidden columns
        ///// </summary>
        //private void RemoveHiddenColumns()
        //{
        //    for (int i = _report.Columns.Count - 1; i >= 0; i--)
        //    {
        //        ReportColumn c = _report.Columns[i];
        //        //if (!c.Visible) _report.Columns.Remove(c);
        //    }
        //}
        ///// <summary>
        ///// Generate table columns
        ///// </summary>
        //private void GenerateTableColumns()
        //{
        //    TableColumn tc = null;

        //    // Shows line numbers if required
        //    if (_report.ShowLineNumbers)
        //    {
        //        // Add a column for the table group
        //        tc = new TableColumn();
        //        this.TableColumns.Add(tc);
        //    }

        //    // add the columns in the order they appear
        //    foreach (ReportColumn c in _report.Columns)
        //    {
        //        tc = new TableColumn(c);
        //        if (!c.Visible) 
        //        {
        //            tc.Visibility.Hidden.Value = "true";
        //            tc.Width = new Size("0");
        //        }

        //        // set conditional visibility for measure columns in crosstab by entity reports
        //        // for hiding non-applicable centers
        //        if (_report.IsEntityByCrosstabReport && c is MeasureColumn && !(c is BlankColumn) )
        //        {
        //            tc.Visibility.Hidden = new Expression(String.Format("=IIF(IsNothing(SUM({0})), True, False)", c.RSFieldName));
        //        }
        //        this.TableColumns.Add(tc);
        //    }
        // }

        ///// <summary>
        ///// Generates table header
        ///// </summary>
        //private void GenerateTableHeader()
        //{
        //    if (_report.IsCrosstabReport)
        //    {
        //        GenerateFirstHeaderRow();
        //        GenerateSecondHeaderRow();
        //    }
        //    else
        //    {
        //        // Table reports have only one header row
        //        GenerateSecondHeaderRow();
        //    }
        //    //ReportColumn col = null;

        //    //// enumerate columns
        //    //for (int i = 0; i < _report.Columns.Count; i++)
        //    //{
        //    //    col = _report.Columns[i];

        //    //    // need to create a header row?
        //    //    if (col.Headers.Count > this.Header.TableRows.Count)
        //    //        GenerateHeaderRows(col.Headers.Count - this.Header.TableRows.Count);

        //    //    // enumerate headers within a column
        //    //    for (int j = 0; j < col.Headers.Count; j++)
        //    //    {
        //    //        Debug.Assert(this.Header.TableRows[j].TableCells[i + _report.ColumnOffset].ReportItems.Count == 1, String.Format(Resources.NO_REPORT_ITEM, j, i));
        //    //        Debug.Assert(this.Header.TableRows[j].TableCells[i + _report.ColumnOffset].ReportItems[0] is Textbox, String.Format(Resources.NO_TEXTBOX_REPORT_ITEM, j, i));

        //    //        TableRow r = this.Header.TableRows[j];
        //    //        Textbox t = (Textbox)r.TableCells[i + _report.ColumnOffset].ReportItems[0];
        //    //        t.CanGrow = col.Headers[j].CanGrow;
        //    //        // resize the row if needed
        //    //        if (col.Headers[j].Height.Unit.Value > r.Height.Unit.Value) r.Height = col.Headers[j].Height;
        //    //        t.Width = new Size((double)col.Width);
        //    //        t.Value = col.Headers[j].Value;
        //    //        t.Style = col.Headers[j].Style;
        //    //    }
        //    //}
        //}
        ///// <summary>
        ///// Generate the first header row of a crosstab report
        ///// </summary>
        //private void GenerateFirstHeaderRow()
        //{
        //    TableCell cell = null;
        //    TableRow r = null;
        //    r = new TableRow();
        //    this.Header.TableRows.Add(r);

        //    // Generate an empty textbox for line number if applicable            
        //    if (_report.ShowLineNumbers)
        //    {
        //        cell = new TableCell(String.Format("{0}_{1}_{2}", Header.PREFIX + "1", Textbox.PREFIX, 0), this.TableColumns[0]);
        //        r.TableCells.Add(cell);
        //    }

        //    // Generate non-measure headers
        //    for (int i = 0; i < _report.Columns.Count;i++ )
        //    {
        //        ReportColumn c = _report.Columns[i];
        //        if (!(c is MeasureColumn))
        //        {
        //            cell = new TableCell(String.Format("{0}_{1}_{2}", Header.PREFIX + "1", Textbox.PREFIX, i + _report.ColumnOffset),
        //                this.TableColumns[i + _report.ColumnOffset]);
        //            ((Textbox)cell.ReportItems[0]).SetCrosstabFirstRowHeaderIdentifierStyle(c);
        //            // just generate emtpy textboxes for non-measure columns
        //            r.TableCells.Add(cell);
        //        }
        //    }
        //    // Generate measure headers
        //    int spanCellCount = _report.IsItemByCrosstabReport ? _report.Entities.Count : _report.CrosstabUniqueColumnCount;

        //    for (int i=0; i < spanCellCount; i++)
        //    {
        //        if (_report.IsItemByCrosstabReport)
        //        {
        //            cell = GenerateSpanCell(_report.Entities[i].Key, _report.Entities[i].Value);
        //            ((Textbox)cell.ReportItems[0]).SetCrosstabHeaderStyle(true);
        //            r.TableCells.Add(cell);
        //        }
        //        else
        //        {
        //            ReportColumnCollection measures = _report.Columns.Filter(typeof(MeasureColumn));
        //            MeasureColumn measure = (MeasureColumn)measures[(i) * _report.Entities.Count];
        //            cell = GenerateSpanCell(measure);
        //            ((Rectangle)cell.ReportItems[0]).SetHeaderStyle(measure);
        //            r.TableCells.Add(cell);
        //            r.Height = _report.Columns.MaxHeaderHeight; //resize the row to accomodate all headers
        //        }
        //    }
        //}

        ///// <summary>
        ///// Generate the second (first for regular report) header row for a crosstab report
        ///// </summary>
        //private void GenerateSecondHeaderRow()
        //{
        //    TableRow r = null;
        //    r = new TableRow();
        //    //resize the row to accomodate all headers
        //    //if (_report.IsItemByCrosstabReport) 
        //    r.Height = _report.Columns.MaxHeaderHeight; 
        //    this.Header.TableRows.Add(r);

        //    ReportColumnCollection measureColumns = _report.Columns.Filter(typeof(MeasureColumn));
            

        //    // Generate an empty textbox for line number if applicable
        //    if (_report.ShowLineNumbers)
        //    {
        //        r.TableCells.Add(new TableCell(String.Format("{0}_{1}_{2}", Header.PREFIX + "2", Textbox.PREFIX, 0), this.TableColumns[0]));
        //    }

        //    for (int i=0; i<_report.Columns.Count; i++)
        //    {
        //        ReportColumn c = _report.Columns[i];

        //        if (c is MeasureColumn && _report.IsMeasureByCrosstabReport)
        //        {
        //            int index = measureColumns.IndexOf(c);
        //            // need to display items
        //            string item = _report.Entities[index % _report.Entities.Count].Value;
        //            Textbox t = Textbox.GetStandardTextBox(String.Format("{0}_{1}_{2}_{3}", Header.PREFIX + "2", Textbox.PREFIX, item.Replace(" ", String.Empty), i.ToString()), item);
        //            t.SetCrosstabHeaderStyle(false);
        //            t.SetHeaderStyle(c);
        //            t.Width = new Size((double)c.Width);
        //            t.Height = r.Height;
        //            r.TableCells.Add(new TableCell(t));
                    
        //        }
        //        else
        //        {
        //            Rectangle rec = c.GetHeadersAsRectangle();
        //            rec.SetHeaderStyle(c);
        //            r.TableCells.Add(new TableCell(rec));
        //        }
        //    }

        //}

        ///// <summary>
        ///// Generates a table cell that spans multiple columns for crosstab ByItem reports
        ///// </summary>
        ///// <param name="itemName">The item name</param>
        ///// <returns></returns>
        //private TableCell GenerateSpanCell(string itemKey, string itemName)
        //{
        //    TableCell tc = new TableCell();
        //    Textbox t = Textbox.GetStandardTextBox(String.Format("{0}_{1}_{2}", Header.PREFIX + "2", Textbox.PREFIX, itemKey), itemName);
        //    t.Width = _report.Columns.MeasureColumnSize(_report.Entities.Count);
        //    t.Style.TextAlign = new Expression(ReportColumn.AlignmentEnum.Center.ToString());
        //    tc.ColSpan = _report.CrosstabUniqueColumnCount;
        //    tc.ReportItems.Add(t);
        //    // TODO How to format item header cell?

        //    return tc;
        //}

        ///// <summary>
        ///// Generate table groups
        ///// </summary>
        //private void GenerateTableGroups()
        //{
        //    ReportGroupCollection groups = null;

        //    groups = _report.ReportGroups.Filter(typeof(StaticGroup));

        //    // Generate static groups
        //    foreach (ReportGroup group in groups)
        //    {
        //        if (group is StaticGroup ) GenerateStaticGroup((StaticGroup)group);
        //    }
        //    // Generate dynamic groups
        //    groups = _report.ReportGroups.Filter(typeof(DynamicGroup));
        //    foreach (ReportGroup group in groups)
        //    {
        //        if (group is DynamicGroup && group.Visible.ToLower()!="false") GenerateDynamicGroup((DynamicGroup)group);
        //    }
        //    // Generate table footer
        //    groups = _report.ReportGroups.Filter(typeof(GrandTotal));
        //    if (groups.Count == 1 && groups[0].Visible != "false")
        //    {
        //        GenerateTableFooter((GrandTotal)groups[0]);
        //    }
        //}

  
        ///// <summary>
        ///// Generate a table group
        ///// </summary>
        ///// <param name="groupName">The name of the table group</param>
        ///// <param name="groupExpression">The group expression</param>
        ///// <returns></returns>
        //private TableGroup GenerateTableGroup(ReportGroup group)
        //{
        //    TableGroup g = new TableGroup(group.ID, group.GroupExpression);
        //    // Add the field to the dataset if it doesn't exist
            
        //    g.Grouping.PageBreakAtStart = group.PageBreakAtStart;
        //    g.Grouping.PageBreakAtEnd = group.PageBreakAtEnd;

        //    // Group header
        //    if (group.Header.Hidden.ToLower()!="true" )
        //    {
        //        g.Header = new Header();
        //        g.Header.RepeatOnNewPage = group.RepeatGroupHeader;

        //        // generate a blank line if needed
        //        if (group.Header.BlankLineBefore.ToLower() != "false")
        //        {
        //            g.Header.TableRows.Add(GenerateBlankLine(group, String.Format("{0}{1}{2}", Textbox.PREFIX, Header.PREFIX, group.ID), true));
        //        }

        //        TableRow r = GenerateGroupRow(g, group, true);
        //        // set conditional header visibility
        //        //applix 39067 
        //        if (group is StaticGroup)
        //            if (group.Header.Hidden.ToLower() != "false") r.Visibility = new Visibility(group.Header.Hidden);
        //        //r.Height = new Size(group.RowHeight);
        //        g.Header.TableRows.Add(r);
        //    }

        //    // Group footer
        //    if (group.Footer.Hidden.ToLower() != "true")
        //    {
        //        g.Footer = new Footer();
        //        g.Footer.RepeatOnNewPage = group.RepeatGroupFooter;
        //        TableRow r = GenerateGroupRow(g, group, false);
        //        // set conditional header visibility
        //        //applix 39067 
        //        if (group is StaticGroup)
        //            if (group.Footer.Hidden.ToLower() != "false") r.Visibility = new Visibility(group.Footer.Hidden);
        //        //r.Height = new Size(group.RowHeight);
        //        g.Footer.TableRows.Add(r);

        //        // generate a blank line if needed
        //        if (group.Footer.BlankLineAfter.ToLower() != "false")
        //        {
        //            g.Footer.TableRows.Add(GenerateBlankLine(group, String.Format("{0}{1}{2}", Textbox.PREFIX, Footer.PREFIX, group.ID), false));
        //        }

        //    }
        //    return g;
        //}



        //// Generates an empty line
        //private TableRow GenerateBlankLine(ReportGroup group, string namePrefix, bool header)
        //{
        //    // check if the blank line contains an expression
        //    bool result = header?bool.TryParse(group.Header.BlankLineBefore, out result):
        //        bool.TryParse(group.Footer.BlankLineAfter, out result);

        //    TableRow row = new TableRow(this.TableColumns.Count, new Size(TableRow.DEFAULT_HEIGHT), namePrefix);
        //    if (!result) row.Visibility.Hidden = new Expression(header? group.Header.BlankLineBefore : group.Footer.BlankLineAfter);
        //    return row;

        //}
        ///// <summary>
        ///// Generates a group footer row to display the group totals
        ///// </summary>
        ///// <param name="g">The table group the row should be added to.</param>
        ///// <param name="fieldName">Used to generate the name of the empty cells.</param>
        ///// <param name="header">If the row is a group header row</param>
        //private TableRow GenerateGroupRow(TableGroup tableGroup, ReportGroup reportGroup, bool header)
        //{
        //    TableRow r = null;
        //    Textbox t = null;
        //    ReportColumn col = null;
        //    string name, value = null;
        //    string fieldName = String.Format("{0}_{1}", reportGroup.ID, header?Header.PREFIX:Footer.PREFIX);

        //    r = new TableRow(this.TableColumns.Count, new Size(header ? reportGroup.Header.RowHeight : reportGroup.Footer.RowHeight));

        //    for (int i = 0; i < this.TableColumns.Count; i++)
        //    {
        //        t = (Textbox)r.TableCells[i].ReportItems[0];

        //        // Configure the first column (line number)
        //        if (i < _report.ColumnOffset)
        //        {
        //            name = String.Format("{0}_{1}_{2}", Textbox.PREFIX, fieldName, i.ToString());
        //            t.Name = name;
        //            continue;
        //        }
        //        col = _report.Columns[i - _report.ColumnOffset];
        //        name = String.Format("{0}_{1}_{2}", Textbox.PREFIX, fieldName, col.ID);
        //        t.Name = name;

        //        // Displays header/footer text
        //        if (col is IdentifierColumn && ((IdentifierColumn)col).IsNameIdentifier)
        //        {
        //            t.CanGrow = true;

        //            if (header)
        //                value = GetGroupHeaderText(tableGroup, reportGroup);
        //            else // footer
        //            {
        //                //t = MergeCells(col, r);
        //                value = GetGroupFooterText(tableGroup, reportGroup);
        //                //if (reportGroup is DynamicGroup) IndentText(reportGroup, t);
        //            }
                    
        //            t.Value = new Expression(value);
        //        }
        //        else
        //        {
        //            // regular column
        //            if (!header)
        //            {
        //                string aggregate = col.GetGroupAggregationFunction(reportGroup, this.Report);
        //                if (aggregate != null)
        //                {
        //                    t.Value = new Expression(aggregate);
                            
        //                }

        //            }
        //            t.Format(col, _report, _report.IsFormulaReport?false:true);

        //        }
        //        t.SetStyle(reportGroup, col, header);
        //    }

        //    // suppress the row for dynamic groups with row count of 1
        //    //if (reportGroup is DynamicGroup)
        //    //{
        //        // start applix 39067
        //        string hiddenExpression = string.Empty;
        //        if (header)
        //            hiddenExpression = reportGroup.Header.Hidden.Trim();
        //        else
        //            hiddenExpression = reportGroup.Footer.Hidden.Trim();

        //        if (hiddenExpression.ToLower() != "false")
        //        {
        //            if (hiddenExpression.StartsWith("="))
        //                hiddenExpression = hiddenExpression.Remove(0, 1);
        //        }

        //        r.Visibility.Hidden = new Expression(String.Format("=IIF(CountRows(\"{0}\")=1, True, " + hiddenExpression + ")", tableGroup.Grouping.Name));
        //        reportGroup.Footer.BlankLineAfter = String.Format("=IIF(CountRows(\"{0}\")=1, True, " + hiddenExpression + ")", tableGroup.Grouping.Name);

        //        //r.Visibility.Hidden = new Expression(String.Format("=IIF(CountRows(\"{0}\")=1, True, False)", tableGroup.Grouping.Name));
        //        //reportGroup.Footer.BlankLineAfter = String.Format("=IIF(CountRows(\"{0}\")=1, True, False)", tableGroup.Grouping.Name);
        //        // end applix 39067
        //    //}
        //    return r;
        //}

        //private bool IsNameColumn(TableColumn tc, ReportGroup rg)
        //{
        //    return this.TableColumns.IndexOf(tc) == _report.ColumnOffset;
        //}

        //private void IndentText(ReportGroup g, Textbox t)
        //{
        //    // determine the group index
        //    ReportGroupCollection groups = _report.ReportGroups.Filter(typeof(DynamicGroup));
        //    int index = groups.IndexOf(g);

        //    t.Style.PaddingLeft = new Expression(String.Format("{0}pt", (index + 1) * DynamicGroup.INDENTATION_LEVEL));

        //}
        //private string GetGroupHeaderText(TableGroup tableGroup, ReportGroup reportGroup)
        //{
        //    if (reportGroup is DynamicGroup)
        //        return _report.Columns.IsNameIdentifierFound ? tableGroup.Grouping.GroupExpressions[0].Value.ToString() : null;
        //    else
        //        return reportGroup.Header.Caption;
        //        //return tableGroup.Grouping.GroupExpressions[0].Value.ToString();
        //}
        //private string GetGroupFooterText(TableGroup tableGroup, ReportGroup reportGroup)
        //{
        //    if (reportGroup is DynamicGroup)
        //        return _report.Columns.IsNameIdentifierFound ? tableGroup.Grouping.GroupExpressions[0].Value.ToString() : null;
        //    else
        //        return reportGroup.Footer.Caption;
        //        //return String.Format("=\"TOTAL \" & {0}", tableGroup.Grouping.GroupExpressions[0].Value.ToString().Replace("=", String.Empty)); ;
        //}

        ///// <summary>
        ///// Generate table details
        ///// </summary>
        //private void GenerateTableDetails()
        //{
        //    Textbox t = null;
        //    TableRow r = new TableRow(this.TableColumns.Count);
        //    r.Height = new Size(_report.Columns.RowHeight);

        //    string fieldValue = null;

        //    // Line Numbers on?
        //    if (_report.ShowLineNumbers)
        //    {
        //        t = (Textbox)r.TableCells[0].ReportItems[0];
        //        t.SetLineNumberTextBox();
        //    }

        //    // Generate custom columns
        //    for (int i = 0; i < _report.Columns.Count; i++)
        //    {
        //        ReportColumn col = _report.Columns[i];

        //        t = (Textbox)r.TableCells[i + _report.ColumnOffset].ReportItems[0];
        //        string fieldName = String.Format("{0}_{1}", Textbox.PREFIX, col.ID);

        //        //if (col is CalculatedColumn)
        //        //    fieldValue = "=" + ((CalculatedColumn)col).GetCalculatedExpression();
        //        //else
        //        fieldValue = (col is BlankColumn) ? null : String.Format("=Fields!{0}.Value", col.ID);

        //        t.Name = fieldName;
        //        t.Value = new Expression(fieldValue);
        //        t.SetStyle(col);
        //        t.Format(col, _report, false);
        //        //if (col.IsNumeric) t.Style.TextAlign = new Expression("Right");
        //        fieldName = String.Format("{0}", col.ID);

        //        // Add the field to the dataset
        //        if (!(col is BlankColumn)) _report.DataSets[this.DataSetName].Fields.Add(new Field(fieldName, fieldName, col.DataType));
        //    }
        //    // resize the row if needed
        //    //if (_report.Columns.RowHeight.Value > r.Height.Unit.Value)
        //    //{
                
        //    //}

        //    this.Details.TableRows.Add(r);
        //}
        ///// <summary>
        ///// Generate table footer
        ///// </summary>
        //private void GenerateTableFooter(GrandTotal group)
        //{
        //    this.Footer = new Footer();
        //    TableRow r = GenerateTableFooterRow(group);
        //    this.Footer.TableRows.Add(r);
        //    //this.Height += r.Height;
        //}
        ///// <summary>
        ///// Generates a table footer row to display the table totals
        ///// </summary>
        ///// <param name="fieldName"></param>
        //private TableRow GenerateTableFooterRow(GrandTotal group)
        //{
        //    TableRow r = null;
        //    Textbox t = null;
        //    ReportColumn col = null;
        //    string name, value;

        //    r = new TableRow(this.TableColumns.Count);

        //    for (int i = 0; i < this.TableColumns.Count; i++)
        //    {
        //        t = (Textbox)r.TableCells[i].ReportItems[0];

        //        // Configure the first column (line number)
        //        if (i == 0 && _report.ColumnOffset > 0)
        //        {
        //            name = String.Format("{0}_{1}{2}_{3}", Textbox.PREFIX, Footer.PREFIX, this.Name, i.ToString());
        //            t.Name = name;
        //            continue;
        //        }

        //        col = _report.Columns[i - _report.ColumnOffset];
        //        name = String.Format("{0}_{1}{2}_{3}", Textbox.PREFIX, Footer.PREFIX, this.Name, col.ID);
        //        t.Name = name;

        //        // The first column (after the line number) 
        //        if (i == _report.ColumnOffset)
        //        {
        //            value = group.Name;
        //            t.Value = new Expression(value);
        //            t.Style.FontWeight = new Expression("Bold");
                    
        //        }
        //        else
        //        {
        //            // merge the cells for two adjacent name columns
        //            //if (col is IdentifierColumn) MergeCells(col, r);

        //            string aggregate = col.GetGroupAggregationFunction(null, this.Report);
        //            // if _report is numeric we need to sum it
        //            if (aggregate != null)
        //            {
        //                // if it is numeric we need to sum it
        //                //value = String.Format("={0}(Fields!{1}.Value)", col.Aggregation.ToString(), col.ID);
        //                t.Value = new Expression(aggregate);
        //                t.SetStyle(group, col, false);
        //                //t.Style.FontWeight = new Expression("Bold");
        //                //t.Style.BorderStyle.Top = new Expression("Solid");
        //                //t.Format(col, _report, true);
        //            }
        //        }
        //    }
        //    return r;
        //}

        //private Textbox MergeCells(ReportColumn c, TableRow r)
        //{
        //    int i = _report.Columns.IndexOf(c) + _report.ColumnOffset;

        //    if (i > 0 && _report.Columns[i - 1] is IdentifierColumn && ((IdentifierColumn)c).IsNameIdentifier)
        //    {
        //        // span the previous cell
        //        r.TableCells[i - 1].ColSpan = 2;

        //        // remove  the containing cell
        //        r.TableCells.Remove(r.TableCells[i]);
        //        return (Textbox) r.TableCells[i - 1].ReportItems[0];
        //    }
        //    return (Textbox)r.TableCells[i].ReportItems[0];
        //}
   
        //#endregion
    }
}