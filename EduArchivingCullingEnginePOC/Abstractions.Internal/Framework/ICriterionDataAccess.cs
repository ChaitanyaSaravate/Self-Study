using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Internal.Framework
{
    public interface ICriterionDataAccess
    {
        int AgeGreaterThan { get; set; }

        DateTime CompletedSchoolBefore { get; set; }
    }
}
