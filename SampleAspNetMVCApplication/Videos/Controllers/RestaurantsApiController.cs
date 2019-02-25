using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.DAL;
using Common.Models;

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

		public IList<Restaurant> GetRestaurants()
		{
			var restaurants = this.dbContext.Restaurants.ToList();
			return restaurants;
		}
	}
}