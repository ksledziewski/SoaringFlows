using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SoaringFlows.Core.Interfaces;

namespace SoaringFlows.Core.Loaders
{
    /// <summary>
    /// Loads object to file
    /// </summary>
    public class JsonFileLoader<T>: IDataLoader<T>
    {
        private readonly string _outputPath;

        // Hidden default ctor
        public JsonFileLoader() { }
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="outputPath">Output path</param>
        public JsonFileLoader(string outputPath)
        {
            _outputPath = outputPath;
        }
        
        /// <summary>
        /// Loads data into destination
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Operation result</returns>
        public OperationResult Load(List<T> input)
        {
            // TODO: Add exception handling
            var serializedInput = JsonConvert.SerializeObject(input);
            if(File.Exists(_outputPath))
                File.Delete(_outputPath);

            using StreamWriter sw = new StreamWriter(_outputPath);
            sw.Write(serializedInput);

            return new OperationResult() { Result = true };
        }
    }
}