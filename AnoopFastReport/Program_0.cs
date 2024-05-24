
using System.Collections.Generic;

using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using Microsoft.Extensions.Configuration;


namespace AnoopFastreport
{
	class Program
	{
		static void Main(string[] args)
		{
			FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));

			var conf = new ConfigurationBuilder()
				 .AddJsonFile("AnoopFastreport.json", false)
				 .Build();

			var conf_params = conf.GetSection("params").GetChildren();

			var report = new Report();
			report.Load(@conf["input"]);
			// report.Dictionary.RegisterData(conf, "conf", true);
			// report.Load(Path.Combine(dir, "Text.frx"));

			foreach (var conf_param in conf_params)
			{
				report.SetParameterValue(conf_param.Key, conf_param.Value);
			}

			// Console.WriteLine(conf_params);

			// var conn = new MySqlDataConnection();
			// conn.ConnectionString = conf["dsn"];
			// conn.CreateAllTables();
			// report.Dictionary.Connections.Add(conn);

			// report.Dictionary.AssignAll();

			// report.RegisterData(conn.DataSet, true);

			report.Prepare();

			var pdfExport = new PDFSimpleExport();
			pdfExport.Export(report, conf["output"]);

			// Console.WriteLine(conf.ToString());

		}
	}
}
