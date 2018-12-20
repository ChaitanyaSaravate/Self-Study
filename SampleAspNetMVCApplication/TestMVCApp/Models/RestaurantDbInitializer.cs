using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestMVCApp.Models
{
	public class RestaurantDbInitializer : DropCreateDatabaseIfModelChanges<RestaurantContext>
	{
		public override void InitializeDatabase(RestaurantContext context)
		{
			List<Restaurant> restaurants = new List<Restaurant>
			{
				new Restaurant{ Address = "FC Road", Name = "Chaitanya Paratha", City = "Pune"},
				new Restaurant{ Address = "Katraj", Name = "Basuri", City = "Pune"},
				new Restaurant{ Address = "Kharadi", Name = "Red Chillies", City = "Pune" },
				new Restaurant{ Address = "Warje", Name = "Swarna", City = "Pune" }
			};

			context.Restaurants.AddRange(restaurants);

			base.InitializeDatabase(context);
		}
	}
}