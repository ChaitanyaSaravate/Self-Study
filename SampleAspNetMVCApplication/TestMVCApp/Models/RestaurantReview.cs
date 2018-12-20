using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMVCApp.Models
{
	public class RestaurantReview
	{
		public int Id { get; set; }
		public int RestaurantId { get; set; }

		[Range(1,10)]
		[Required]
		public int Rating { get; set; }

		public string Body { get; set; }

		[Display(Name = "Reviewer Name")]
		public string ReviewerName { get; set; }
	}
}