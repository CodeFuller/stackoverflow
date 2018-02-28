using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;

namespace ConsoleApplication
{
	class HasIndexers
	{
		public object this[string s] => null;

		public object this[int i] => null;
	}

	class Program
	{
		static void Main(string[] args)
		{
			var p = Expression.Parameter(typeof(int), "p");
			Expression assignment = Expression.Assign(p, Expression.Constant(1));
			Expression addAssignment = Expression.AddAssign(p, Expression.Constant(5));
			BlockExpression addAssignmentBlock = Expression.Block(
				new ParameterExpression[] { p },
				assignment, addAssignment);

			SyntaxTree tree1 = addAssignmentBlock.ToSyntaxTree();

			Expression<Func<bool>> expr = () => new HasIndexers()[3] == new HasIndexers()["three"];
			SyntaxTree tree2 = expr.ToSyntaxTree();
		}
	}
}
