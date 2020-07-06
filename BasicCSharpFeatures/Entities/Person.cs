namespace Entities
{
    public class Person
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public string Summary { get; set; }

        public void GeneratePersonSummary()
        {
            this.Summary = Name + Address;
        }
    }
}
