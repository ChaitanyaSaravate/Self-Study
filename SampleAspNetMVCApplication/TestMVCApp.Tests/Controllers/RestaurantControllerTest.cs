using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Common.DAL;
using Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestMVCApp.Controllers;
using TestMVCApp.Models;

namespace TestMVCApp.Tests.Controllers
{
	[TestClass]
	public class RestaurantControllerTest
	{
		[TestMethod]
		public void Test_IndexActionReturnsAllRestaurants()
		{
			ViewResult result = this.GetRestaurantController().Index() as ViewResult;
			
			Assert.IsInstanceOfType(result.ViewData.Model, typeof(IQueryable<RestaurantViewModel>));

			IEnumerable<RestaurantViewModel> restaurants = result.Model as IEnumerable<RestaurantViewModel>;
			Assert.AreEqual(6, restaurants.Count());
			Assert.AreEqual(5, restaurants.First().Id);
			Assert.AreEqual(9, restaurants.First().AverageRating);
			Assert.AreEqual("Test Name 5", restaurants.First().Name);
			Assert.AreEqual(3, restaurants.First().TotalReviews);
		}

		[TestMethod]
		public void Test_IndexActionReturnsRestaurantsWhoseNamesStartWithTheSearchTerm()
		{
			var searchTerm = "Different";
			ViewResult result = this.GetRestaurantController().Index(searchTerm) as ViewResult;

			var searchResult = result.Model as IEnumerable<RestaurantViewModel>;

			Assert.AreEqual(2, searchResult.Count());
			Assert.AreEqual(0, searchResult.Count(r => !r.Name.StartsWith(searchTerm)));
		}

		private RestaurantController GetRestaurantController()
		{
			var mockDbContext = new Mock<IRestaurantContext>();
			mockDbContext.Setup(c => c.Restaurants).Returns(GetMockRestaurantDbSet().Object);

			RestaurantController controller = new RestaurantController(mockDbContext.Object);
			controller.ControllerContext = new FakeControllerContext();

			return controller;
		}

		private Mock<DbSet<Restaurant>> GetMockRestaurantDbSet()
		{
			var mockRestaurantSet = new Mock<DbSet<Restaurant>>();

			var mockRestaurants = GetRestaurants();
			mockRestaurantSet.As<IQueryable<Restaurant>>().Setup(rest => rest.ElementType)
				.Returns(mockRestaurants.ElementType);
			mockRestaurantSet.As<IQueryable<Restaurant>>().Setup(rest => rest.Expression)
				.Returns(mockRestaurants.Expression);
			mockRestaurantSet.As<IQueryable<Restaurant>>().Setup(rest => rest.Provider)
				.Returns(mockRestaurants.Provider);
			mockRestaurantSet.As<IQueryable<Restaurant>>().Setup(rest => rest.GetEnumerator()).Returns(mockRestaurants.GetEnumerator);

			return mockRestaurantSet;
		}

		private IQueryable<Restaurant> GetRestaurants()
		{
			return new List<Restaurant>
			{
				new Restaurant { Address = "Test Address 1", Id = 1, Name = "Test Name 1", City = "Test City 1", Reviews = new List<RestaurantReview>() },
				new Restaurant { Address = "Test Address 2", Id = 2, Name = "Test Name 2", City = "Test City 2", Reviews = new List<RestaurantReview>() },
				new Restaurant { Address = "Test Address 3", Id = 3, Name = "Test Name 3", City = "Test City 3", Reviews = new List<RestaurantReview>() },
				new Restaurant { Address = "Test Address 4", Id = 4, Name = "Test Name 4", City = "Test City 4", Reviews = new List<RestaurantReview>() },
				new Restaurant { Address = "Test Address 5", Id = 5, Name = "Test Name 5", City = "Test City 5", Reviews = new List<RestaurantReview>
				{
					new RestaurantReview { Body = "Great", Id = 1, Rating = 10, RestaurantId = 5, ReviewerName = "Chaitanya"},
					new RestaurantReview { Body = "Good", Id = 2, Rating = 8, RestaurantId = 5, ReviewerName = "Kedar"},
					new RestaurantReview { Body = "Best", Id = 3, Rating = 9, RestaurantId = 5, ReviewerName = "Amey"}
				} },
				new Restaurant { Address = "Test Address 6", Id = 6, Name = "Different Name 6", City = "Test City 6", Reviews = new List<RestaurantReview>
				{
					new RestaurantReview { Body = "Great", Id = 1, Rating = 10, RestaurantId = 5, ReviewerName = "Chaitanya"},
					new RestaurantReview { Body = "Good", Id = 2, Rating = 8, RestaurantId = 5, ReviewerName = "Kedar"},
					new RestaurantReview { Body = "Best", Id = 3, Rating = 8, RestaurantId = 5, ReviewerName = "Amey"}
				} },
				new Restaurant { Address = "Test Address 7", Id = 7, Name = "Different Name 7", City = "Test City 7", Reviews = new List<RestaurantReview>
				{
					new RestaurantReview { Body = "Great", Id = 1, Rating = 10, RestaurantId = 5, ReviewerName = "Chaitanya"},
					new RestaurantReview { Body = "Good", Id = 2, Rating = 5, RestaurantId = 5, ReviewerName = "Kedar"},
					new RestaurantReview { Body = "Best", Id = 3, Rating = 6, RestaurantId = 5, ReviewerName = "Amey"}
				} },

			}.AsQueryable();
		}
	}
}
