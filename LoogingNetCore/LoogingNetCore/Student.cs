namespace LoggingNetCore
{
    public class Student
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string SSN { get; set; }

        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
