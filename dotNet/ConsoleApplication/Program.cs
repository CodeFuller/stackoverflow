using System;
using Microsoft.Office.Interop.PowerPoint;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var powerPointApp = new Microsoft.Office.Interop.PowerPoint.Application();
			var presentation = powerPointApp.Presentations.Open(@"Presentation.pptx");
			var slide = presentation.Slides[1];
			var shape = slide.Shapes[1];

			Chart chart = shape.Chart;
			ChartData chartData = chart.ChartData;
			chartData.Activate();

			var workbook = chartData.Workbook;
			var dataSheet = workbook.Worksheets[1];

			double[] newData = { 1, 2, 3, 4, 5 };

			var colNumber = 2;
			var firstRowNumber = 2;

			//	Clearing previous data
			dataSheet.UsedRange.Columns[colNumber, Type.Missing].Clear();

			for (var i = 0; i < newData.Length; ++i)
			{
				dataSheet.Cells[firstRowNumber + i, colNumber] = newData[i];
			}

			//	Saving and closing Excel workbook before presentation could be saved.
			workbook.Close(true);
			presentation.Save();
			presentation.Close();
			powerPointApp.Quit();
		}
	}
}
