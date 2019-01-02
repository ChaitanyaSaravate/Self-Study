using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVCApp.Models
{
	public interface IRestaurantContext
	{
		DbSet<Restaurant> Restaurants { get; set; }

		DbSet<RestaurantReview> RestaurantReviews { get; set; }

		void SaveChanges();
	}
}
