using System.ComponentModel.DataAnnotations;

namespace TestMVCApp.Models
{
	public class RestaurantViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		[Display(Name = "Total Reviews")]
		public int TotalReviews { get; set; }

		[Display(Name = "Average Rating")]
		public double? AverageRating { get; set; }
	}
}