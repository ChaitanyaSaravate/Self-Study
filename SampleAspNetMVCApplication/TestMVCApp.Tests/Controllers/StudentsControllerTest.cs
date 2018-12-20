using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestMVCApp.Controllers;
using TestMVCApp.Models;

namespace TestMVCApp.Tests.Controllers
{
	[TestClass]
	public class StudentsControllerTest
	{
		[TestMethod]
		public void SearchTest()
		{
			StudentsController studentsController = new StudentsController();
			
		//	CheckIfViewAndModelIsNotNull(studentsController.Search(2) as ViewResult);
			// CheckIfViewAndModelIsNotNull(studentsController.Search() as ViewResult);

			List<Student> students = new List<Student>
			{
				new Mock<Student>(MockBehavior.Default).Object,
				new Mock<Student>(MockBehavior.Default).Object,
				new Mock<Student>(MockBehavior.Default).Object
			};

			var mockStudent = new Mock<Student>(MockBehavior.Default);
			mockStudent.SetupAllProperties();


			studentsController.ListAll();

		}

		private void CheckIfViewAndModelIsNotNull(ViewResult result)
		{
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Model);
		}
	}
}
