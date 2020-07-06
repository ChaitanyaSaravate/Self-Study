using System.Collections.Generic;

namespace LoggingNetCore
{
    public class Order
    {
        public int ID { get; set; }

        public IEnumerable<Student> OrderItems { get; set; }

        public string Customer { get; set; }
    }
}
