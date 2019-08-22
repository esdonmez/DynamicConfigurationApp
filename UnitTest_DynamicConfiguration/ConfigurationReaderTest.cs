using Lib_DynamicConfiguration;
using System;
using Xunit;

namespace UnitTest_DynamicConfiguration
{
    public class ConfigurationReaderTest
    {
        private readonly ConfigurationReader _configurationReader;


        public ConfigurationReaderTest()
        {
            _configurationReader = new ConfigurationReader("test", "mongodb:localhost:27017/ConfigReader", 45000);
        }


        [Fact]
        public async void AddNullInput()
        {
            var result = await _configurationReader.AddAsync(null);
            Assert.False(result);
        }

        [Fact]
        public async void UpdateNullInput()
        {
            var result = await _configurationReader.UpdateAsync(null);
            Assert.False(result);
        }

        [Fact]
        public async void GetByIdNullInput()
        {
            var result = await _configurationReader.GetByIdAsync(null);
            Assert.Null(result);
        }

        [Fact]
        public async void SearchByNameNullInput()
        {
            var result = await _configurationReader.SearchByNameAsync(null);
            Assert.Empty(result);
        }
    }
}
