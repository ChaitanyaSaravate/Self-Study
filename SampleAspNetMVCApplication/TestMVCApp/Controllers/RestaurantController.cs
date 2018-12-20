using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMVCApp.Models;

namespace TestMVCApp.Controllers
{
	public class RestaurantController : Controller
	{
		RestaurantContext dbContext = new RestaurantContext();

		public ActionResult Index()
		{
			List<Restaurant> restaurants = dbContext.Restaurants.ToList();
			return View(restaurants);
		}

		public ActionResult Details(int id)
		{
			return View(this.dbContext.Restaurants.Find(new object[] { id }));
		}

		public ActionResult Edit(int id)
		{
			Restaurant restaurant = this.dbContext.Restaurants.Find(new object[] { id });
			return View(restaurant);
		}
		[HttpPost]
		public ActionResult Update([Bind(Exclude = "Name")]Restaurant restaurant)
		{
			// Note that though the action name is "Update", the view from it is called is "Edit". This is possible.
			// Note that the we have 'excluded' Name property from binding. So MVC will not send changes in the Name property when this action is called.
			Restaurant restaurant1 = this.dbContext.Restaurants.Find(new object[] { restaurant.Id });
			restaurant1.Address = restaurant.Address;
			if (TryUpdateModel(restaurant1))
			{
				this.dbContext.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View("Edit", restaurant);
			}
		}

		public ActionResult Delete(int id)
		{
			var restaurant = this.dbContext.Restaurants.Find(new object[] { id });
			this.dbContext.Restaurants.Remove(restaurant);
			this.dbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Create()
		{
			return View("Edit");
		}

		[HttpPost]
		public ActionResult Create(Restaurant restaurant)
		{
			this.dbContext.Restaurants.Add(restaurant);
			this.dbContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}