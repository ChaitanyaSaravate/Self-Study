using System.Web.Mvc;
using Common.DAL;
using Common.Models;
using TestMVCApp.Models;

namespace TestMVCApp.Controllers
{
	public class ReviewsController : Controller
	{
		private RestaurantContext dbContext = new RestaurantContext();
		// GET: RestaurantReviews
		public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
		{
			var restaurant = dbContext.Restaurants.Find(restaurantId);
			return View(restaurant);
		}

		public ActionResult Create(int restaurantId)
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(RestaurantReview restaurantReview)
		{
			if (ModelState.IsValid)
			{
				var restaurant = this.dbContext.Restaurants.Find(restaurantReview.RestaurantId);
				restaurant.Reviews.Add(restaurantReview);
				this.dbContext.SaveChanges();
			}

			return RedirectToAction("Index", new { id = restaurantReview.RestaurantId });
		}

		public ActionResult Edit(int id)
		{
			var review = this.dbContext.RestaurantReviews.Find(id);
			return View(review);
		}

		[HttpPost]
		public ActionResult Edit([Bind(Exclude = "ReviewerName")] RestaurantReview restaurantReview)
		{
			if (ModelState.IsValid)
			{
				var review = this.dbContext.RestaurantReviews.Find(restaurantReview.Id);
				review.Body = restaurantReview.Body;
				review.Rating = restaurantReview.Rating;

				if (TryUpdateModel(review))

				{
					this.dbContext.SaveChanges();
				}
			}

			return RedirectToAction("Index", new { id = restaurantReview.RestaurantId });
		}

		protected override void Dispose(bool disposing)
		{
			this.dbContext.Dispose();
			base.Dispose(disposing);
		}
	}
}