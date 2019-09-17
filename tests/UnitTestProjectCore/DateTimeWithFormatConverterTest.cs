using Helper.JsonConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace UnitTestProjectCore
{
    internal class TesteConverterDefaultFormat
    {
        [JsonConverter(typeof(DateTimeWithFormatConverter))]
        public DateTime Data { get; set; }
    }

    internal class TesteConverterCustomFormat
    {
        [JsonConverter(typeof(DateTimeWithFormatConverter), "dd-MM-yy HH:mm:ss")]
        public DateTime Data { get; set; }
    }

    internal class TestConverterDateTimeInvalid
    {
        [JsonConverter(typeof(DateTimeWithFormatConverter))]
        public string DataFake { get; set; }
    }

    [TestClass]
    public class DateTimeWithFormatConverterTest
    {
        [TestMethod]
        public void DateTimeWithFormatConverter_UsingDefaultFormat()
        {
            string formatExpected = "dd/MM/yyyy HH:mm:ss";

            var currentDate = DateTime.Now;

            TesteConverterDefaultFormat teste = new TesteConverterDefaultFormat { Data = currentDate };

            string json = JsonConvert.SerializeObject(teste);

            Assert.IsTrue(json.Contains(currentDate.ToString(formatExpected)));

        }

        [TestMethod]
        public void DateTimeWithFormatConverter_UsingCustomFormat()
        {
            string formatExpected = "dd-MM-yy HH:mm:ss";

            var currentDate = DateTime.Now;

            TesteConverterCustomFormat teste = new TesteConverterCustomFormat { Data = currentDate };

            string json = JsonConvert.SerializeObject(teste);

            Assert.IsTrue(json.Contains(currentDate.ToString(formatExpected)));

        }

        [TestMethod]
        public void DateTimeWithFormatConverter_DeserializeObject_Null()
        {
            string json = @"{'Data':null}";

            TesteConverterCustomFormat teste = JsonConvert.DeserializeObject<TesteConverterCustomFormat>(json);

            Assert.AreEqual(DateTime.MinValue, teste.Data);
        }

        [TestMethod]
        public void DateTimeWithFormatConverter_DeserializeObject_Ok()
        {
            string json = "{\"Data\":\"17-09-19 14:00:00\"}";

            TesteConverterCustomFormat teste = JsonConvert.DeserializeObject<TesteConverterCustomFormat>(json);

            var expected = "17/09/2019 14:00:00";

            Assert.AreEqual(expected, teste.Data.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR")));
        }

        [TestMethod]
        public void DateTimeWithFormatConverter_SerializeObject_Exception()
        {
            try
            {
                TestConverterDateTimeInvalid converter = new TestConverterDateTimeInvalid { DataFake = "data fake" };
                string json = JsonConvert.SerializeObject(converter);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Campo não é um DateTime.", ex.Message);
            }
        }

    }
    
}
