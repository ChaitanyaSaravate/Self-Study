using System.Collections.Generic;

namespace Common.Models
{
	public class Restaurant
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public virtual ICollection<RestaurantReview> Reviews { get; set; }
	}
}