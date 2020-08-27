using System.Collections.Generic;

namespace SoaringFlows.Core.Interfaces
{
    /// <summary>
    /// Data loader
    /// </summary>
    /// <typeparam name="T">Type of data to load</typeparam>
    public interface IDataLoader<T>
    {
        /// <summary>
        /// Load data into destination
        /// </summary>
        /// <param name="input">Data to load into destination</param>
        /// <returns>Information regarding loading process</returns>
        OperationResult Load(List<T> input);
    }
}