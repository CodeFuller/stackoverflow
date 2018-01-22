using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NetCore.WebApiApplication
{
	public class MyCustomAuthorizationService : IAuthorizationService
	{
		private readonly DefaultAuthorizationService innerProvider;

		private readonly IAuthorizationPolicyProvider _policyProvider;
		private readonly IAuthorizationHandlerProvider _handlers;
		private readonly IAuthorizationHandlerContextFactory _contextFactory;
		private readonly IAuthorizationEvaluator _evaluator;
		private readonly AuthorizationOptions _options;

		public MyCustomAuthorizationService(IAuthorizationPolicyProvider policyProvider, IAuthorizationHandlerProvider handlers, ILogger<DefaultAuthorizationService> logger, IAuthorizationHandlerContextFactory contextFactory, IAuthorizationEvaluator evaluator, IOptions<AuthorizationOptions> options)
		{
			innerProvider = new DefaultAuthorizationService(policyProvider, handlers, logger, contextFactory, evaluator, options);

			_policyProvider = policyProvider;
			_handlers = handlers;
			_contextFactory = contextFactory;
			_evaluator = evaluator;
			_options = options.Value;
		}

		public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
		{
			if (requirements == null)
			{
				throw new ArgumentNullException(nameof(requirements));
			}

			var authContext = _contextFactory.CreateContext(requirements, user, resource);
			var handlers = await _handlers.GetHandlersAsync(authContext);
			foreach (var handler in handlers)
			{
				await handler.HandleAsync(authContext);
				if (!_options.InvokeHandlersAfterFailure && authContext.HasFailed)
				{
					break;
				}
			}

			var userIsAnonymous =
				user?.Identity == null ||
				!user.Identities.Any(i => i.IsAuthenticated);


			var result = _evaluator.Evaluate(authContext);
			return result;
		}

		public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
		{
			return innerProvider.AuthorizeAsync(user, resource, policyName);
		}
	}
}
