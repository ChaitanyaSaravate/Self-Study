using System.Data.Entity;

namespace TestMVCApp.Models
{
	public class RestaurantContext : DbContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }

		public RestaurantContext() : base("RestaurantDb")
		{
			Database.SetInitializer(new RestaurantDbInitializer());
		}

		public System.Data.Entity.DbSet<TestMVCApp.Models.RestaurantReview> RestaurantReviews { get; set; }
	}
}