using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.DAL;
using TestMVCApp.Models;

namespace RestaurantServices.Controllers
{
	public class RestaurantsApiController : ApiController
	{
		private IRestaurantContext dbContext;

		public RestaurantsApiController()
		{
			dbContext = new RestaurantContext();
		}

		public RestaurantsApiController(IRestaurantContext restaurantContext)
		{
			dbContext = restaurantContext;
		}

		public IList<RestaurantViewModel> GetRestaurants()
		{
			var restaurants = this.dbContext.Restaurants
				.OrderByDescending(r => r.Reviews.Count > 0 ? r.Reviews.Average(rev => rev.Rating) : 0).Select(rest =>
					new RestaurantViewModel
					{
						Id = rest.Id,
						Address = rest.Address,
						City = rest.City,
						Name = rest.Name,
						TotalReviews = rest.Reviews.Count,
						AverageRating = rest.Reviews.Count > 0 ? rest.Reviews.Average(rev => rev.Rating) : 0
					});
			
			return restaurants.ToList();
		}

		[ActionName("InCity")]
		public IList<RestaurantViewModel> GetRestaurantsInCity(string city)
		{
			var restaurantsInCity = this.dbContext.Restaurants.Where(r => city == null || city.Trim() == String.Empty || r.City.Equals(city, StringComparison.InvariantCultureIgnoreCase)).Select(r => new RestaurantViewModel
			{
				Id = r.Id,
				Address = r.Address,
				City = r.City,
				Name = r.Name,
				TotalReviews = r.Reviews.Count,
				AverageRating = r.Reviews.Count > 0 ? r.Reviews.Average(rev => rev.Rating) : 0
			});

			return restaurantsInCity.ToList();
		}
	}
}