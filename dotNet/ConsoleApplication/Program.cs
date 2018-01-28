using Microsoft.Office.Interop.Word;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Application application = new Application();
			Document document = application.Documents.Open(@"d:\temp\test.docx");

			Range range = document.Range();

			document.Close();
			application.Quit();
		}
	}
}
