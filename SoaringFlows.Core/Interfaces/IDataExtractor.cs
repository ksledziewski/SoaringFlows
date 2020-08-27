using System.Collections.Generic;

namespace SoaringFlows.Core.Interfaces
{
    /// <summary>
    /// Interface for data extractors
    /// </summary>
    public interface IDataExtractor<T>
    {
        /// <summary>
        /// Extract data from the datasource
        /// </summary>
        /// <returns>Data retrieved from source</returns>
        List<T> Extract();
    }
}