using System;
using Abstractions.Internal.Framework;

namespace DAL
{
    public class CriterionDataAccess : ICriterionDataAccess
    {
        public int AgeGreaterThan
        {
            get => 21;
            set => value = 21;
        }

        public DateTime CompletedSchoolBefore
        {
            get => DateTime.Now;
            set => value = DateTime.Now;
        }
    }
}
