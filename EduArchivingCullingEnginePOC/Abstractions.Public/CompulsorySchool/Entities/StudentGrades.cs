﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Public.CompulsorySchool.Entities
{
    /// <summary>
    /// Student Info With Grades
    /// </summary>
    public class StudentGrades : StudentInfo
    {
        public List<Grade> Grades { get; set; }
    }
}
