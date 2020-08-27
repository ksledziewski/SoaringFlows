using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoaringFlows.Core.Interfaces;

namespace SoaringFlows.Core.Extractors
{
    /// <summary>
    /// Data extractor for Csv files
    /// </summary>
    /// <typeparam name="T">Type of object to be returned by the extractor</typeparam>
    public class FileCsvExtractor<T> : IDataExtractor<T>
    {
        private readonly string _inputPath;
        private readonly Func<List<List<string>>, List<T>> _map;
        private readonly char _separator;

        /// <summary>
        /// Default C-tor hidden
        /// </summary>
        private FileCsvExtractor() {}
        
        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="inputPath">Input path for file</param>
        /// <param name="separator">Separator used to split data</param>
        /// <param name="map">Mapping function</param>
        public FileCsvExtractor(string inputPath, char separator, Func<List<List<string>>, List<T>> map)
        {
            _inputPath = inputPath;
            _map = map;
            _separator = separator;
        }

        /// <summary>
        /// Extract data from file and map to object
        /// </summary>
        /// <returns>Extracted data</returns>
        /// <exception cref="ArgumentException"></exception>
        public List<T> Extract()
        {
            if (string.IsNullOrEmpty(_inputPath))
                throw new ArgumentException($"Empty input file path provided!");

            var streamReader = File.OpenText(_inputPath);
            var lines = new List<string>();
            var outputLines = new List<List<string>>();
            
            while (!streamReader.EndOfStream)
                lines.Add(streamReader.ReadLine());
            
            lines.ForEach(x => 
                outputLines.Add(x.Split(_separator).ToList()));
            
            return _map(outputLines);
        }
    }
}