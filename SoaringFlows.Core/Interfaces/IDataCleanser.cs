using System.Collections.Generic;

namespace SoaringFlows.Core.Interfaces
{
    /// <summary>
    /// Data cleanser for extracted data
    /// </summary>
    public interface IDataCleanser<T>
    {
        /// <summary>
        /// Cleanse data 
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Cleansed data</returns>
        List<T> Cleanse(List<T> input);
    }
}