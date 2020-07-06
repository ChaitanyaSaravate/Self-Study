using System.Collections.Generic;
using Abstractions.Public.KAA.Entities;

namespace Abstractions.Public.KAA
{
    public interface IArchiveReminders
    {
        IEnumerable<Reminders> GetReminders(GetKAARequest requestedData);
    }
}
