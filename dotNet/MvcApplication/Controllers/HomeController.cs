using System;
using System.Net;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[HttpPut]
		public IActionResult Put([FromBody] School school)
		{
			try
			{
				var schoolExists = _schoolRepository.SchoolExists(school.Id);

				if (!schoolExists) return NotFound();

				if (!ModelState.IsValid) return BadRequest();

				var schoolData = Mapper.Map<School, Data.School>(school);

				var updatedClass = _schoolRepository.UpdateSchool(schoolData);

				if (!updatedClass) return Json(GetHttpResponseMessage(HttpStatusCode.InternalServerError));

				var route = CreatedAtRoute("GetSchool", school);

				return route;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}
	}
}