using System.Collections.Generic;

namespace LoggingNetCore
{
    public class School
    {
        public IList<Student> Students { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public School(string name, int id)
        {
            Name = name;
            Id = id;
            Students = new List<Student>
            {
                new Student{Name = "Chaitanya Saravate", SSN = "12345678", Address = "Pune", Gender = Gender.Male},
                new Student{Name = "Rahul Mahulkar", SSN = "87654321", Address = "Pune", Gender = Gender.Male},
            };
        }

        public School()
        {

        }
    }
}
