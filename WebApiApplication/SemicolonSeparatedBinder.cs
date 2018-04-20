namespace WebApiApplication
{
	public class SemicolonSeparatedBinder : SymbolSeparatedBinder
	{
		protected override char Separator => ';';
	}
}
