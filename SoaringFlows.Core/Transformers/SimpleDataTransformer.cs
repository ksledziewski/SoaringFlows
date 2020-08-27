using System;
using System.Collections.Generic;
using SoaringFlows.Core.Interfaces;

namespace SoaringFlows.Core.Transformers
{
    /// <summary>
    /// Converts U data type to T data type
    /// </summary>
    /// <typeparam name="U">Input type</typeparam>
    /// <typeparam name="T">Output type</typeparam>
    public class SimpleDataTransformer<U,T>: IDataTransformer<U, T>
    {
        private readonly Func<List<U>, List<T>> _map;

        // Hidden default ctor
        private SimpleDataTransformer() { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="map">Mapping function</param>
        public SimpleDataTransformer(Func<List<U>, List<T>> map)
        {
            _map = map;
        }
        
        /// <summary>
        /// Transform data
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Transformed data</returns>
        public List<T> Transform(List<U> input)
        {
            return _map(input);
        }
    }
}