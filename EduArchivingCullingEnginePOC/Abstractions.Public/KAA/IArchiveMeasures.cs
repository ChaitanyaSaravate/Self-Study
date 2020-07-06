using System.Collections.Generic;
using Abstractions.Public.KAA.Entities;

namespace Abstractions.Public.KAA
{
    public interface IArchiveMeasures
    {
        IEnumerable<Measures> GetMeasures(GetKAARequest requestedData);
    }
}
