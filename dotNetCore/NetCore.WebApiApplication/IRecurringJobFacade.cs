using System;
using System.Linq.Expressions;

namespace NetCore.WebApiApplication
{
	public interface IRecurringJobFacade
	{
		void AddOrUpdate(Expression<Action> methodCall, Func<string> cronExpression);

		//	Mimic other methods from RecurringJob that you are going to use.
		// ...
	}
}
