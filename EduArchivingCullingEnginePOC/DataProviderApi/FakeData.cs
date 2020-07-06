using System.Collections.Generic;
using Abstractions.Public.CompulsorySchool.Entities;
using Abstractions.Public.KAA.Entities;

namespace DataProviderApi
{
    internal static class FakeData
    {
        internal static List<StudentInfo> GetStudentsInfo()
        {
            var students = new List<StudentInfo>();

            for (var i = 23; i < 100; i++)
            {
                students.Add(new StudentInfo { Age = i, Id = i + 500, Name = $"Chaitanya - {i}" });
            }

            return students;
        }

        internal static List<StudentGrades> GetStudentGrades()
        {
            var students = new List<StudentGrades>();

            for (var i = 23; i < 100; i++)
            {
                students.Add(new StudentGrades { Age = i, Id = i + 500, Name = $"Chaitanya - {i}", Grades = new List<Grade>()});
            }

            foreach (var student in students)
            {
                student.Grades.Add(new Grade { Subject = "Science", Class = 1 });
                student.Grades.Add(new Grade { Subject = "Maths", Class = 2 });
            }
            
            return students;
        }

        internal static List<StudentAbsences> GetStudentAbsences()
        {
            var students = new List<StudentAbsences>();

            for (var i = 23; i < 100; i++)
            {
                students.Add(new StudentAbsences() { Age = i, Id = i + 500, Name = $"Chaitanya - {i}", Absences = new List<Absence>()});
            }

            foreach (var student in students)
            {
                student.Absences.Add(new Absence { Subject = "Science", WasPresent = true });
                student.Absences.Add(new Absence { Subject = "Maths", WasPresent = false });
            }

            return students;
        }

        internal static List<Measures> GetMeasures()
        {
            List<Measures> measures = new List<Measures>();

            for (int i = 1; i < 100; i++)
            {
                var measure = new Measures
                {
                    Measurement = 1,
                    Name = "Test",
                    Youth = new YouthBase
                    {
                        Age = 25,
                        Name = $"Sourabh - {i}",
                        Id = i
                    }
                };
                measures.Add(measure);
            }

            return measures;
        }

        internal static List<Reminders> GetReminders()
        {
            List<Reminders> measures = new List<Reminders>();

            for (int i = 1; i < 100; i++)
            {
                var measure = new Reminders
                {
                    Youth = new YouthBase
                    {
                        Age = 25,
                        Name = $"Sourabh - {i}",
                        Id = i
                    },
                    Title = $"Reminder - {i}",
                    Details = $"This is Reminder # {i}"
                };
                measures.Add(measure);
            }

            return measures;
        }
    }
}
