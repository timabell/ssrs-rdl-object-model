using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Reporting.ObjectModel;
using System.IO;
using System.Xml;
using System.Globalization;
using RS=TestHarness.ReportingService2005;

namespace TestHarness
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Textbox t;
            Report report = new Report();
            // Report-level properties
            report.Language = "en-US";
            report.Width = new Reporting.ObjectModel.Size(6.5);
            report.Author = "Teo Lachev";
            report.LeftMargin = report.TopMargin = report.RightMargin = report.BottomMargin = new Reporting.ObjectModel.Size(.5);
            // Data source
            DataSource dataSource = new DataSource("AdventureWorks");
            dataSource.DataSourceReference = "AdventureWorks";
            report.DataSources.Add(dataSource);
            // Dataset
            DataSet ds = new DataSet("Main");
            ds.Query = new Query("SELECT ProductID, Name, ProductNumber, StandardCost FROM Production.Product WHERE StandardCost > 0 ORDER BY Name");
            ds.Query.DataSourceName = "AdventureWorks";
            ds.Fields.Add(new Field("ProductID", "ProductID", typeof(System.Int32)));
            ds.Fields.Add(new Field("Name", "Name", typeof(System.String)));
            ds.Fields.Add(new Field("ProductNumber", "ProductNumber", typeof(System.String)));
            ds.Fields.Add(new Field("StandardCost", "StandardCost", typeof(System.Double)));
            report.DataSets.Add(ds);
            // Body
            report.Body.Height = new Reporting.ObjectModel.Size("6.5");
            // Table
            Table table = new Table("tblMain");
            report.Body.ReportItems.Add(table);
            table.Top = new Reporting.ObjectModel.Size(.5);
            table.DataSetName = "Main";
            // Table columns
            table.TableColumns.Add(new TableColumn(1));
            table.TableColumns.Add(new TableColumn(2));
            table.TableColumns.Add(new TableColumn(1));
            table.TableColumns.Add(new TableColumn(1));
            // Table header
            TableRow r = new TableRow(4, new Reporting.ObjectModel.Size(.25), "h_t");
            table.Header.TableRows.Add(r);
            t = ((Textbox)r.TableCells[0].ReportItems[0]);
            t.Value.Value = "ProductID";
            t.Style.FontWeight.Value = "Bold";
            t = ((Textbox)r.TableCells[1].ReportItems[0]);
            t.Value.Value = "Name";
            t.Style.FontWeight.Value = "Bold";
            t = ((Textbox)r.TableCells[2].ReportItems[0]);
            t.Value.Value = "Number";
            t.Style.FontWeight.Value = "Bold";
            t = ((Textbox)r.TableCells[3].ReportItems[0]);
            t.Value.Value = "Cost";
            t.Style.FontWeight.Value = "Bold";
            t.Style.TextAlign.Value = "Right";
            // Table details
            r = new TableRow(4, new Reporting.ObjectModel.Size(.25), "t");
            table.Details.TableRows.Add(r);
            t = ((Textbox)r.TableCells[0].ReportItems[0]);
            t.Value.Value = "=Fields!ProductID.Value";
            t.Style.TextAlign.Value = "Left";
            t = ((Textbox)r.TableCells[1].ReportItems[0]);
            t.Value.Value = "=Fields!Name.Value";
            t = ((Textbox)r.TableCells[2].ReportItems[0]);
            t.Value.Value = "=Fields!ProductNumber.Value";
            t = ((Textbox)r.TableCells[3].ReportItems[0]);
            t.Value.Value = "=Fields!StandardCost.Value";
            // Table footer
            r = new TableRow(4, new Reporting.ObjectModel.Size(.25), "f_t");
            table.Footer.TableRows.Add(r);
            t = ((Textbox)r.TableCells[3].ReportItems[0]);
            t.Value.Value = "=SUM(Fields!StandardCost.Value)";
            t.Style.FontWeight.Value = "Bold";
            t.Style.TextAlign.Value = "Right";

            // Serialize the report to file
            File.WriteAllBytes("RDLObjectModelDemo.rdl", report.ToByteArray());

            // Deserialize the report
            XmlReader reader = XmlReader.Create("RDLObjectModelDemo.rdl");
            report = new Report(reader);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool forRendering = false;
            string historyID = null;
            RS.ParameterValue[] values = null;
            RS.DataSourceCredentials[] credentials = null;
            string reportPath = "/AdventureWorks Sample Reports/Employee Sales Summary";

            // parameters for Employee Sales Summary report
            RS.ReportingService2005 managementProxy = new RS.ReportingService2005();
            managementProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            RS.ReportParameter[] parameters = managementProxy.GetReportParameters(reportPath, historyID, forRendering, values, credentials);

            parameters[0].Name = "ReportMonth";
            parameters[0].DefaultValues[0] = "12";
            parameters[1].Name = "ReportYear";
            parameters[1].DefaultValues[0] = "2003";
            parameters[2].Name = "EmpID";
            parameters[2].DefaultValues = new string[]{"288"};
            historyID = RunSnapshotReport(reportPath, parameters); 
        }

        /// <summary>
        /// Genererates a snapshot report
        /// </summary>
        /// <param name="reportPath">The report path</param>
        /// <param name="reportParameters">The report default params</param>
        /// <returns>The snapshot history id</returns>
        private string RunSnapshotReport(string reportPath, RS.ReportParameter[] parameters)
        {
            string historyID = null;
            RS.Warning[] warnings = null;
            RS.ScheduleDefinitionOrReference schedule = null;

            RS.ReportingService2005 managementProxy = new RS.ReportingService2005();
            managementProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            // Check the execution options of the report (live or snapshot)
            RS.ExecutionSettingEnum execitionOption = managementProxy.GetExecutionOptions(reportPath, out schedule);

            if (execitionOption == RS.ExecutionSettingEnum.Live) throw new ApplicationException(String.Format("Report {0} is not configured for snapshot execution", reportPath));

            // All parameters have to be assigned default values before snapshot is generated 
            if (parameters != null) managementProxy.SetReportParameters(reportPath, parameters);

            // Create the report snapshot
            managementProxy.UpdateReportExecutionSnapshot(reportPath);

            // Check if the report is configured to keep snapshots in history
            bool keepExecutionShapshots = false;
            bool result = managementProxy.GetReportHistoryOptions(reportPath, out keepExecutionShapshots, out schedule);

            if (keepExecutionShapshots)
            {
                // history is automatically created, get the list of history runs
                RS.ReportHistorySnapshot[] history = managementProxy.ListReportHistory(reportPath);
                // Need to sort by date since history runs may not be chronologically sorted
                Array.Sort(history, CompareReportHistoryByDate); 
                historyID = history[history.Length - 1].HistoryID; //grab the last history run
            }
            else
            {
                // explicitly create history snapshot
                historyID = managementProxy.CreateReportHistorySnapshot(reportPath, out warnings);
            }

            return historyID;
        }
        private static int CompareReportHistoryByDate(RS.ReportHistorySnapshot x, RS.ReportHistorySnapshot y)
        {
            return x.CreationDate < y.CreationDate ? -1 : 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentUICulture;
            string cis = Shared.Util.StringFromObject(ci);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportViewerDemo rv = new ReportViewerDemo();
            rv.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}