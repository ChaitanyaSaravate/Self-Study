using System.Data.Entity;
using Common.Models;

namespace Common.DAL
{
	public interface IRestaurantContext
	{
		DbSet<Restaurant> Restaurants { get; set; }

		DbSet<RestaurantReview> RestaurantReviews { get; set; }

		void SaveChanges();
	}
}
