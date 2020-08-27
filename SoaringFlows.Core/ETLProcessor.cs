using SoaringFlows.Core.Interfaces;

namespace SoaringFlows.Core
{
    /// <summary>
    /// Process single ETL operation
    /// </summary>
    public class ETLProcessor<T,U>
    {
        private IDataExtractor<T> _extractor;
        private IDataCleanser<T> _cleanser;
        private IDataTransformer<T, U> _transformer;
        private IDataLoader<U> _loader;

        // Hidden default ctor
        private ETLProcessor() { }
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="extractor">Data extractor</param>
        /// <param name="cleanser">Data cleanser</param>
        /// <param name="transformer">Data transformation</param>
        /// <param name="loader">Data loader</param>
        public ETLProcessor(
            IDataExtractor<T> extractor, 
            IDataCleanser<T> cleanser, 
            IDataTransformer<T, U> transformer, 
            IDataLoader<U> loader)
        {
            _extractor = extractor;
            _cleanser = cleanser;
            _transformer = transformer;
            _loader = loader;
        }
        
        /// <summary>
        /// Process single ETL operation
        /// </summary>
        /// <returns>Information regarding ETC process</returns>
        public OperationResult Process()
        {
            var extracted = _extractor.Extract();
            var cleansed = _cleanser.Cleanse(extracted);
            var transformed = _transformer.Transform(cleansed);
            return _loader.Load(transformed);
        }
    }
}