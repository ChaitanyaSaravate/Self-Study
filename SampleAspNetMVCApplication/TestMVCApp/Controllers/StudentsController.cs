using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestMVCApp.App_Start;
using TestMVCApp.Models;

namespace TestMVCApp.Controllers
{
	public class StudentsController : Controller
	{
		[HttpGet]
		public ActionResult Search(int id = 1)
		{
			// Search action using HttpGet
			return View(GetStudents().Single(s => s.Id == id));
			//return Content(st.First().Name); // Returning Content as the result
		}

		[ActionName("Search")]
		[HttpPost]
		public ActionResult SearchByName(string name) // Name of the method is different from ActionName
		{
			// One more Search action but using HttpPost
			return View(GetStudents().Find(s => s.Name.Equals(name))); // Returning view as the result
		}
		
		[ActionName("Topper")]
		public ActionResult GetTopper()
		{
			var topStudent = from student in GetStudents() orderby student.Rating descending select student;
			return RedirectToAction("Search", new { id = topStudent.First().Id }); // Redirect to action to get the student having top ranking
		}

		public ActionResult ListAll()
		{
			return View(GetStudents());
		}

		public ActionResult Edit(int id)
		{
			var student = from s in GetStudents() where s.Id == id select s;
			return View(student.First());
		}

		[HttpPost]
		public ActionResult Edit(int id, FormCollection formCollection)
		{
			var student = GetStudents().Single(s => s.Id == id);
			if (TryUpdateModel(student))
			{
				return RedirectToAction("ListAll");
			}
			return View(student);
		}

		[ChildActionOnly] //Makes this action available only as a child action from some other part of application. User cannot invoke it directly from the browser.
		public ActionResult ShowTopper()
		{
			var topStudent = from student in GetStudents() orderby student.Rating descending select student;
			return PartialView("_topperBanner", topStudent.First());
		}

		public ActionResult Create()
		{
			return RedirectToRoute("StudentSearch");
		}

		[ActionName("EduPortal")]
		public ActionResult RedirectToEducationPortal()
		{
			return RedirectPermanent("http://www.pluralsight.com"); // Redirect to different website.
		}

		[TokenFilter] // See how the filter can be applied to a specific action
		public ActionResult Stylesheet()
		{
			return File(Server.MapPath("~/Content/Site.css"), "text/css"); // Returning file contents
		}

		private static List<Student> GetStudents()
		{
			return new List<Student>
			{
				new Student {Id = 1, Name = "Kedar", Address = "Pune", Age = 10, Class = "Fourth", RollNumber = 1, Rating = 5},
				new Student {Id = 2, Name = "Chaitanya", Address = "Kolhapur", Age = 11, Class = "Fifth", RollNumber = 2, Rating = 6},
				new Student {Id = 3, Name = "Geeta", Address = "Delhi", Age = 16, Class = "Tenth", RollNumber = 4, Rating = 7},
				new Student {Id = 4, Name = "Amruta", Address = "Mumbai", Age = 6, Class = "First", RollNumber = 10, Rating = 8},
				new Student {Id = 5, Name = "Priyanka", Address = "Pune", Age = 10, Class = "Fourth", RollNumber = 2, Rating = 9}
			};
		}
	}
}