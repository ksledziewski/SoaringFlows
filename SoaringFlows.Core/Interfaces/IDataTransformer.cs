using System.Collections.Generic;

namespace SoaringFlows.Core.Interfaces
{
    /// <summary>
    /// Interface for data transformers
    /// </summary>
    /// <typeparam name="U">Input type</typeparam>
    /// <typeparam name="T">Output type</typeparam>
    public interface IDataTransformer<U,T>
    {
        /// <summary>
        /// Transform data
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Transformed data</returns>
        List<T> Transform(List<U> input);
    }
}