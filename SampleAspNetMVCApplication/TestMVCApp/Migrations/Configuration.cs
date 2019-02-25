using Common.DAL;
using Common.Models;
using TestMVCApp.Models;

namespace TestMVCApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TestMVCApp.Models.RestaurantContext";
        }

        protected override void Seed(RestaurantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

	        for (int i = 10; i < 1000; i++)
	        {
		        context.Restaurants.AddOrUpdate(restaurant => restaurant.Id, new Restaurant
				{
					Address = "J M Road",
					Id = i,
					Name = "Test Restaurant - " + i,
					City = "Pune"
				});
	        }
        }
    }
}
