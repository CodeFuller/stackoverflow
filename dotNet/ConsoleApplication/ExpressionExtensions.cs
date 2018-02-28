using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ConsoleApplication
{
	public static class ExpressionExtensions
	{
		public static SyntaxTree ToSyntaxTree(this Expression expression)
		{
			var expressionCode = expression.ToReadableString();
			return CSharpSyntaxTree.ParseText(expressionCode);
		}
	}
}
