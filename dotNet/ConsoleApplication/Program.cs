using System;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			FileStream stream = File.Open(@"C:\Users\Bumba\Downloads\Sample-Sales-Data.xlsx", FileMode.Open, FileAccess.Read);
			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

			DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
			{
				ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
				{
					UseHeaderRow = true
				}
			});

			foreach (DataTable table in result.Tables)
			{
				foreach (DataRow row in table.Rows)
				{
					var salesRepName = (string)row["Sales_Rep_Name"];
					if (String.Equals(salesRepName, "Janet"))
					{
						var year = (int)(double)row["Year"];
						Console.WriteLine($"Janet's year is {year}");
					}
				}
			}

			foreach (DataTable table in result.Tables)
			{
				foreach (DataRow row in table.Select("Sales_Rep_Name = 'Janet'"))
				{
					var year = (int)(double)row["Year"];
					Console.WriteLine($"Janet's year is {year}");
				}
			}

			excelReader.Close();
		}
	}
}
