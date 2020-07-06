namespace Entities
{
    public class Organization
    {
        public string Name { get; set; }
        public string HeadOffice { get; set; }
    }

    public class School : Organization
    {
        public int Capacity { get; set; }
        public Person Principal { get; set; }
    }

    public class Company : Organization
    {
        public IndustryType IndustryType { get; set; }
        public Person ManagingDirector { get; set; }
    }

    public enum IndustryType
    {
        IT,
        Automobile,
        Pharma
    }
}
