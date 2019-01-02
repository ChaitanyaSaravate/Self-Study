using System.Collections.Generic;
using System.Data.Entity;

namespace TestMVCApp.Models
{
	public class RestaurantContext : DbContext, IRestaurantContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }

		public RestaurantContext() : base("RestaurantDb")
		{
			Database.SetInitializer(new RestaurantDbInitializer());
		}

		public System.Data.Entity.DbSet<TestMVCApp.Models.RestaurantReview> RestaurantReviews { get; set; }

		void IRestaurantContext.SaveChanges()
		{
			((DbContext)this).SaveChanges();
		}
	}
}