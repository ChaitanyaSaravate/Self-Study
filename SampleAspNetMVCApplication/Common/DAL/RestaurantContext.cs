using System.Data.Entity;
using Common.Models;

namespace Common.DAL
{
	public class RestaurantContext : DbContext, IRestaurantContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }

		public RestaurantContext() : base("RestaurantDb")
		{
			Database.SetInitializer(new RestaurantDbInitializer());
		}

		public System.Data.Entity.DbSet<RestaurantReview> RestaurantReviews { get; set; }

		void IRestaurantContext.SaveChanges()
		{
			((DbContext)this).SaveChanges();
		}
	}
}