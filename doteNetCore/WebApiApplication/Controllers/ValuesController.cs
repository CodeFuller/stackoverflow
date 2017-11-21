using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace WebApiApplication.Controllers
{
	public class DateTimeModelBinder : IModelBinder
	{
		private readonly IModelBinder baseBinder = new SimpleTypeModelBinder(typeof(DateTime));

		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueProviderResult != ValueProviderResult.None)
			{
				bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

				var valueAsString = valueProviderResult.FirstValue;

				//	valueAsString will have a string value of your date, e.g. '31-12-2017'
				//	Parse it as you want and build DateTime object
				var dateTime = DateTime.ParseExact(valueAsString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
				bindingContext.Result = ModelBindingResult.Success(dateTime);

				return Task.CompletedTask;
			}

			return baseBinder.BindModelAsync(bindingContext);
		}
	}

	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		//[HttpGet]
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET api/values/5
		[HttpGet]
		public IActionResult Get([FromQuery] [ModelBinder(typeof(DateTimeModelBinder))] DateTime date)
		{
			return null;
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
