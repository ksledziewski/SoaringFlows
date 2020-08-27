using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using SoaringFlows.Core.Cleansers;
using SoaringFlows.Core.Extractors;
using SoaringFlows.Core.Loaders;
using SoaringFlows.Core.Transformers;

namespace SoaringFlows.Core.Test
{
    /// <summary>
    /// Tests for transformations from CSV to JSON file
    /// </summary>
    public class FileCsvToJsonFileTests
    {
        private const string _jsonPath = "../../../Data/gliders.json";

        /// <summary>
        /// Test setup method
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test csv extractor
        /// </summary>
        [Test]
        public void CsvExtractorTest()
        {
            List<Glider> Map(List<List<string>> x) => x
                .Select(y => 
                    new Glider(y[0], Boolean.Parse(y[1])))
                .ToList();

            var extractor = new FileCsvExtractor<Glider>("../../../Data/gliders.csv", ';', Map);
            var extracted = extractor.Extract();

            Assert.AreEqual(3, extracted.Count);
            Assert.AreEqual("Puchacz", extracted[0].Type);
            Assert.AreEqual("Pirat", extracted[1].Type);
            Assert.AreEqual("ASG29", extracted[2].Type);
            
            Assert.Pass();
        }

        /// <summary>
        /// Csv cleanser test
        /// </summary>
        [Test]
        public void CsvCleanserTest()
        {
            var gliders = new List<Glider>()
            {
                new Glider(" Puchacz", false),
                new Glider(" Pirat ", false)
            };
            
            var cleanser = new FileCsvCleanser<Glider>();
            var cleansed = cleanser.Cleanse(gliders);
            
            Assert.AreEqual("Puchacz", cleansed[0].Type);
            Assert.AreEqual("Pirat", cleansed[1].Type);
            
            Assert.Pass();
        }

        /// <summary>
        /// Simple transformer test
        /// </summary>
        [Test]
        public void SimpleTransformerTest()
        {
            var gliders = new List<Glider>()
            {
                new Glider("Puchacz", false),
                new Glider("Pirat", false)
            };

            List<Szybowiec> Map(List<Glider> x) => gliders
                    .Select(y => new Szybowiec(y.Type, y.HasEngine))
                    .ToList();

            var transformer = new SimpleDataTransformer<Glider, Szybowiec>(Map);
            var szybowce = transformer.Transform(gliders);
            
            Assert.AreEqual("Puchacz", szybowce[0].Typ);
            Assert.AreEqual("Pirat", szybowce[1].Typ);
        }

        /// <summary>
        /// Json file loader test
        /// </summary>
        [Test]
        public void JsonFileLoaderTest()
        {
            
            var gliders = new List<Glider>()
            {
                new Glider("Puchacz", false),
                new Glider("Pirat", false)
            };
            
            var jsonLoader = new JsonFileLoader<Glider>(_jsonPath);
            var result = jsonLoader.Load(gliders);
            
            Assert.AreEqual(true, result.Result);

            var glidersJson = File.ReadAllText(_jsonPath);

            var glidersDeserialized = JsonConvert.DeserializeObject<List<Glider>>(glidersJson);
            
            Assert.AreEqual("Puchacz", glidersDeserialized[0].Type);
            Assert.AreEqual("Pirat", glidersDeserialized[1].Type);
        }

        /// <summary>
        /// Test the whole ETL operation
        /// </summary>
        [Test]
        public void FullEtlOperationTest()
        {
            // Define data extractor
            List<Glider> MapExtracted(List<List<string>> x) => x
                .Select(y => 
                    new Glider(y[0], Boolean.Parse(y[1])))
                .ToList();

            var extractor = new FileCsvExtractor<Glider>("../../../Data/gliders.csv", ';', MapExtracted);
            
            // Define data cleanser
            var cleanser = new FileCsvCleanser<Glider>();
            
            // Define data transformer
            List<Szybowiec> MapTransform(List<Glider> x) => x
                .Select(y => new Szybowiec(y.Type, y.HasEngine))
                .ToList();

            var transformer = new SimpleDataTransformer<Glider, Szybowiec>(MapTransform);

            // Define data loader
            var loader = new JsonFileLoader<Szybowiec>(_jsonPath);

            // ETL operation
            var processor = new ETLProcessor<Glider, Szybowiec>(extractor, cleanser, transformer, loader);
            var result = processor.Process();
            
            Assert.AreEqual(true, result.Result);
            
            
            // Check output file
            var glidersJson = File.ReadAllText(_jsonPath);

            var glidersDeserialized = JsonConvert.DeserializeObject<List<Szybowiec>>(glidersJson);
            
            Assert.AreEqual("Puchacz", glidersDeserialized[0].Typ);
            Assert.AreEqual("Pirat", glidersDeserialized[1].Typ);

        }

        /// <summary>
        /// Cleanup after tests
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            if(File.Exists(_jsonPath))
                File.Delete(_jsonPath);
        }
        
        #region === Test data structures ===

        /// <summary>
        /// Test class
        /// </summary>
        private class Glider
        {
            /// <summary>
            /// Type
            /// </summary>
            public string Type { get; set; } 
            /// <summary>
            /// Has engine
            /// </summary>
            public bool HasEngine { get; set; }

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="type">Glider type</param>
            /// <param name="hasEngine">Has engine</param>
            public Glider(string type, bool hasEngine)
            {
                Type = type;
                HasEngine = hasEngine;
            }
        }

        /// <summary>
        /// Test class
        /// </summary>
        private class Szybowiec
        {
            // Type
            public string Typ { get; set; }
            // Has Engine
            public bool MaSilnik { get; set; }

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="typ">Type</param>
            /// <param name="maSilnik">HasEngine</param>
            public Szybowiec(string typ, bool maSilnik)
            {
                Typ = typ;
                MaSilnik = maSilnik;
            }
        }
        
        #endregion
        
    }
}