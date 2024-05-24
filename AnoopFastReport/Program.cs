using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using Microsoft.Extensions.Configuration;

Console.WriteLine("BEGIN");

var conf = new ConfigurationBuilder()
	 .AddJsonFile("AnoopFastreport.json", false)
	 .Build();

// var conn = new MySqlDataConnection();
// conn.ConnectionString = @conf["dbSource"];
// conn.SetName(@conf["dbAlias"]);
// conn.Alias = @conf["dbAlias"];
// conn.CreateAllTables();

var report = new Report();
report.Load(@conf["input"]);

if (@conf["dbDriver"] == "mysql")
{
	FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
	var conn = new MySqlDataConnection();
}
if (@conf["dbDriver"] == "sqlite")
{
	FastReport.Utils.RegisteredObjects.AddConnection(typeof(SQLiteDataConnection));
	var conn = new SQLiteDataConnection();
	conn.ConnectionString = @conf["dbSource"];
	// Console.WriteLine(@conf["dbSource"]);
	// conn.Alias = "conn";
	conn.CreateAllTables();
	conn.CreateAllProcedures();
	// conn.
	// report.Dictionary.Connections.Add(conn);
	// report.RegisterData(conn.DataSet, true);
}

var confParams = conf.GetSection("params").GetChildren();
foreach (var confParam in confParams)
{
	report.SetParameterValue(confParam.Key, confParam.Value);
}
report.SetParameterValue("conn", @conf["dbSource"]);

report.Prepare();

var pDFSimpleExport = new PDFSimpleExport();
pDFSimpleExport.Export(report, @conf["output"]);

Console.WriteLine("END");
