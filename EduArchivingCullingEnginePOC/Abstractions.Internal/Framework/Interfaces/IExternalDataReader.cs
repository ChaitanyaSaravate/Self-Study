using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    /// <summary>
    /// Helps you to connect to data providers and get the data.
    /// </summary>
    public interface IExternalDataReader
    {
        /// <summary>
        /// Read data from external sources.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">Request object containing selection criterion</param>
        /// <param name="entity">Entity for which data is to be downloaded.</param>
        /// <returns>List of data files in which data is downloaded.</returns>
        Task<List<string>> ReadData<T>(T request, EduEntity entity);
    }
}
