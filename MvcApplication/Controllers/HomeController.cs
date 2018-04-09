using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(string id)
		{
			var propertyNames = new List<string>();
			var userProperties = typeof(User).GetProperties();

			foreach (PropertyInfo prop in userProperties)
			{
				Type type = prop.PropertyType;
				if (!(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>)))
				{
					string attrName = string.Empty;
					var attribute = (DisplayNameAttribute)prop.GetCustomAttribute(typeof(DisplayNameAttribute), true);
					if (attribute != null)
					{
						attrName = attribute.DisplayName;
					}
					else
					{
						attrName = prop.Name;
					}

					propertyNames.Add(attrName);
				}
			}
			ViewData["PropertyList"] = propertyNames;

			try
			{
				var user = new User
				{
					Id = 123,
					Name = "John Smith",
					IsActive = true,
				};

				return View(user);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
