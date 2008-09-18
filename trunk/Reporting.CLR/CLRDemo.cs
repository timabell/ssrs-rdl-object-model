using System;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using RDL=Reporting.ObjectModel;
using Microsoft.Xml.Serialization.GeneratedAssembly;

public partial class StoredProcedures
{
    internal static int MAX_PREVIEW_ROWS = 100;

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CLRDemo(SqlXml metadata)
    {
        // Deserialize the report
        ReportSerializer serializer = new ReportSerializer();
        XmlReader reader = metadata.CreateReader();
        RDL.Report report = (RDL.Report)serializer.Deserialize(reader);

        // Generate the table schema
        DataTable table = GenerateSchema(report);

        // Generate preview results
        GeneratePreviewData(table, report);

        if (!SqlContext.IsAvailable) return;

        SqlDataRecord record = TableRowToSqlDataRecord(table);
        SqlContext.Pipe.SendResultsStart(record);

        foreach (DataRow row in table.Rows)
        {
            record.SetValues(row.ItemArray);
            SqlContext.Pipe.SendResultsRow(record);
        }

        SqlContext.Pipe.SendResultsEnd();
        return;
    }



    /// <summary>
    /// Generate the schema for the resultset 
    /// </summary>
    /// <param name="metadata">The Metadata dataset</param>
    /// <returns>DataTable with columns matching the metadata</returns>
    private static DataTable GenerateSchema(RDL.Report report)
    {
        DataTable table = new DataTable();

        // reference the main dataset
        RDL.DataSet ds = report.DataSets[0];

        foreach (RDL.Field f in ds.Fields)
        {
            table.Columns.Add(f.Name, f.TypeName);
        }

        return table;
    }
 

       /// <summary>
    /// Generate preview data 
    /// </summary>
    /// <param name="results">An empty table to hold results</param>
    /// <returns>DataTable with columns matching the metadata</returns>
    private static void GeneratePreviewData(DataTable table, RDL.Report report)
    {
        Random rand = new Random();
     

        for (int i = 0; i < MAX_PREVIEW_ROWS; i++)
        {
            DataRow row = table.NewRow();

            for (int j = 0; j < table.Columns.Count; j++)
            {

                    string fieldName = table.Columns[j].ColumnName;


                    // report column
                    switch (table.Columns[j].DataType.ToString())
                    {
                        case "System.String":
                            {
                                switch (table.Columns[j].ColumnName.ToLower())
                                {
                                    case "entity": row[j] = table.Columns[j].ColumnName + " " + i % 2; break;
                                    case "item": row[j] = table.Columns[j].ColumnName + " " + i % 2; break;
                                    default: row[j] = table.Columns[j].ColumnName + "_" + i.ToString(); break;
                                }
                                break;
                            }
                        case "System.Decimal": row[j] = (i % 5 == 0 ? -1 : 1) * 100000 * rand.NextDouble(); break;
                        case "System.Double": row[j] = (i % 5 == 0 ? -1 : 1) * 100000 * rand.NextDouble(); break;
                        case "System.Int32": row[j] = (i % 5 == 0 ? -1 : 1) * rand.Next(10000); break;
                    }

            }

            table.Rows.Add(row);
        }
    }

    /// <summary>
    /// Generates SqlRecord metadata from an ADO.NET table
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    internal static SqlDataRecord TableRowToSqlDataRecord(DataTable table)
    {
        SqlMetaData[] meta = new SqlMetaData[table.Columns.Count];

        DataTable schemaTable = table.CreateDataReader().GetSchemaTable();
        SqlString collationString = new SqlString(String.Empty);

        for (int i = 0; i < meta.Length; i++)
        {
            DataRow columnSchema = schemaTable.Rows[i];
            DataColumn column = table.Columns[i];

            switch (column.DataType.ToString().ToLower())
            {
                case "system.int32":
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.Int); break;
                    }
                case "system.string":
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.NVarChar, column.MaxLength); break;
                    }
                case "system.decimal":
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.Decimal, 19, 4); break;
                    }
                case "system.double":
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.Float); break;
                    }
                case "system.guid":
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.UniqueIdentifier); break;
                    }
                case "system.datetime":
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.DateTime); break;
                    }
                default:
                    {
                        meta[i] = new SqlMetaData(column.Caption, SqlDbType.NVarChar, column.MaxLength); break;
                    }
            }
        }
        SqlDataRecord rec = new SqlDataRecord(meta);
        return rec;
    }

};
