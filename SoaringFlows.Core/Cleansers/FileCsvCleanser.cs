using System.Collections.Generic;
using System.Linq;
using SoaringFlows.Core.Interfaces;

namespace SoaringFlows.Core.Cleansers
{
    /// <summary>
    /// Cleanser for extracted data from Csv file
    /// </summary>
    public class FileCsvCleanser<T>: IDataCleanser<T>
    {
        /// <summary>
        /// Cleanse data
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Cleansed data</returns>
        public List<T> Cleanse(List<T> input)
        {
            return RemoveWhiteSpaces(input);
            
            // TODO: Other cleaning data operations to be added here
        }

        // Clean white spaces from input
        private List<T> RemoveWhiteSpaces(List<T> input)
        {
            input.ForEach(x =>
            {
                var data = typeof(T)
                    .GetProperties()
                    .ToList()
                    .Where(y => y.PropertyType == typeof(string))
                    .ToList();
                
                data.ForEach(d =>
                {
                    var value = ((string) d.GetValue(x)).Trim();
                    d.SetValue(x, value);
                });
            });
            
            return input;
        }
    }
}