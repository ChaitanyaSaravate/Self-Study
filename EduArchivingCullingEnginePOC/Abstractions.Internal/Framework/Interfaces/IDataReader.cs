using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    /// <summary>
    /// Helps you to connect to data providers and get the data.
    /// </summary>
    public interface IDataReader
    {
        Task<bool> ReadData<T>(T request, EduEntity entity);
    }
}
