namespace WebApiApplication
{
	public class CommaSeparatedBinder : SymbolSeparatedBinder
	{
		protected override char Separator => ',';
	}
}
