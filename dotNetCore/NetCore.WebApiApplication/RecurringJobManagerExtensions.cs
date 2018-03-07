using System;
using System.Linq.Expressions;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;

namespace NetCore.WebApiApplication
{
	public static class RecurringJobManagerExtensions
	{
		public static void AddOrUpdate(this IRecurringJobManager manager, Expression<Action> methodCall, Func<string> cronExpression, TimeZoneInfo timeZone = null, string queue = EnqueuedState.DefaultQueue)
		{
			var job = Job.FromExpression(methodCall);
			var id = $"{job.Type.ToGenericTypeString()}.{job.Method.Name}";

			manager.AddOrUpdate(id, job, cronExpression(), timeZone ?? TimeZoneInfo.Utc, queue);
		}
	}
}
