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

    }
}
